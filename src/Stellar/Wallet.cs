using OneOf;
using OneOf.Types;
using StellarDotnetSdk;
using StellarDotnetSdk.Accounts;
using StellarDotnetSdk.Assets;
using StellarDotnetSdk.Operations;
using StellarDotnetSdk.Requests;
using StellarDotnetSdk.Responses;
using StellarDotnetSdk.Responses.Results;
using StellarDotnetSdk.Transactions;
using Tommy;

namespace Stellar;

public class ServerSubmissionException : Exception
{
    public ServerSubmissionException()
        : base("Failed to submit transaction")
    {
    }

    public required SubmitTransactionResponse Response { get; init; }
}

public record Wallet(
    string NetworkUrl,
    OneOf<string, None> AccountSecretSeed,
    OneOf<HomeDomain, None> HomeDomain) : IDisposable
{
    public static readonly (string Mainnet, string Testnet) PublicNetworkUrl = ("https://horizon.stellar.org", "https://horizon-testnet.stellar.org");
    
   public OneOf<Network, None> NetworkInfo { get; private set; } = new None();

    public bool IsInitialized { get; private set; }

    public Lazy<Server> Server { get; } = new(() => new Server(NetworkUrl));
    
    public OneOf<AccountResponse, None> Account { get; private set; } = new None();

    public void Dispose()
    {
        // Dispose of unmanaged resources.
        Dispose(true);

        // Suppress finalization.
        GC.SuppressFinalize(this);
    }

    private bool _disposed;
    protected virtual void Dispose(bool deterministicDisposal)
    {
        if (_disposed)
        {
            return;
        }

        // disposed managed objects
        if (deterministicDisposal)
        {
            if (Server.IsValueCreated)
            {
                Server.Value.Dispose();
            }
        }

        _disposed = true;
    }

    public static void AutoSign(Transaction transaction, Wallet submittingAccount)
    {
        var requiredSignatures =
            transaction.Operations.Select(o => o.SourceAccount)
            .Union(new[] { transaction.SourceAccount })
            .Where(account => account != null)
            .Select(account => account!.AccountId)
            .Distinct()
            .ToList();

        var availableSigners =
            transaction.Operations.Select(o => o.SourceAccount)
                .Union(new[] { transaction.SourceAccount, GetSigner(submittingAccount.AccountSecretSeed).Match(keypair => (KeyPair?)keypair, none => null) })
                .Where(account => account != null && account.SigningKey.CanSign())
                .Cast<IAccountId>()
                .DistinctBy(account => account.AccountId)
                .ToList();

        foreach (var requiredSignature in requiredSignatures)
        {
            var availableSigner = availableSigners.FirstOrDefault(signer => signer.AccountId == requiredSignature);
            if (availableSigner != null)
            {
                transaction.Sign(availableSigner, submittingAccount.NetworkInfo.AsT0);
            }
        }
    }

    private static async Task AutoSignAsync(Transaction transaction, Network network, string networkUrl, HttpClient httpClient)
    {
        var requiredSignatures =
            transaction.Operations.Select(o => o.SourceAccount)
                .Union(new[] { transaction.SourceAccount })
                .Where(account => account != null)
                .Select(account => account!.AccountId)
                .Distinct()
                .ToList();

        var availableSigners =
            transaction.Operations.Select(o => o.SourceAccount)
                .Union(new[] { transaction.SourceAccount })
                .Where(account => account != null && account.SigningKey.CanSign())
                .Cast<IAccountId>()
                .DistinctBy(account => account.AccountId)
                .ToList();

        var responseHandler = new ResponseHandler<RootResponse>();
        using var response = await httpClient.GetAsync(networkUrl);
        var rootResponse = await responseHandler.HandleResponse(response);
        var networkInfo = new Network(rootResponse.NetworkPassphrase);

        foreach (var requiredSignature in requiredSignatures)
        {
            var availableSigner = availableSigners.FirstOrDefault(signer => signer.AccountId == requiredSignature);
            if (availableSigner != null)
            {
                transaction.Sign(availableSigner, networkInfo);
            }
        }
    }

    private static OneOf<StellarDotnetSdk.Accounts.KeyPair, None> GetSigner(OneOf<string, None> seed)
    {
        try
        {
            return StellarDotnetSdk.Accounts.KeyPair.FromSecretSeed(seed.AsT0);
        }
        catch (Exception)
        {
        }

        return new None();
    }

    public static async Task<SubmitTransactionResponse?> SubmitTransactionAsync(
        Transaction transaction,
        bool autoSign,
        Network network,
        string networkUrl,
        string logDescription)
    {
        try
        {
            using var httpClient = new HttpClient();

            // sign transaction
            if (autoSign)
            {
                await AutoSignAsync(transaction, network, networkUrl, httpClient);
            }

            // submit transaction
            var server = new Server(networkUrl, httpClient);
            var response = await server.SubmitTransaction(transaction, new SubmitTransactionOptions { EnsureSuccess = true });
            if (response?.IsSuccess is true)
            {
                Console.WriteLine($"{logDescription} - Success");
            }
            else
            {
                throw new ServerSubmissionException() { Response = response! };
            }

            return response;
        }
        catch (ServerSubmissionException)
        {
            throw;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"{logDescription} - Exception: {exception.Message + Environment.NewLine + exception.StackTrace}");
            throw;
        }
    }

    public async Task<SubmitTransactionResponse?> SubmitTransactionAsync(Transaction transaction, bool autoSign, string logDescription)
    {
        try
        {
            // sign transaction
            if (autoSign)
            {
                AutoSign(transaction, this);
            }

            // submit transaction
            var response = await Server.Value.SubmitTransaction(transaction, new SubmitTransactionOptions { EnsureSuccess = true });
            if (response?.IsSuccess is true)
            {
                Console.WriteLine($"{logDescription} - Success");
            }
            else
            {
                throw new ServerSubmissionException() { Response = response! };
            }

            return response;
        }
        catch (ServerSubmissionException)
        {
            throw;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"{logDescription} - Exception: {exception.Message + Environment.NewLine + exception.StackTrace}");
            throw;
        }
    }

    private async Task<string> InitializeInternalAsync(string accountId)
    {
        if (IsInitialized)
        {
            return accountId;
        }

        // KeyPair uses NSec.Cryptography.Sodium.InitializeCore, which is not supported on Blazor WebAssembly.
        // We will initialize as much as we can while avoiding this library.
        var canUseCryptography = CanUseCryptography();

        // get account info
        Account = await Server.Value.Accounts.Account(canUseCryptography && AccountSecretSeed.IsT0 ? StellarDotnetSdk.Accounts.KeyPair.FromSecretSeed(AccountSecretSeed.AsT0).AccountId : accountId);

        // get network info
        NetworkInfo = new Network((await this.Server.Value.RootAsync()).NetworkPassphrase);

        // set home domain
        if (Account.MapT0(account => account.HomeDomain).TryPickT0(out var existingHomeDomain, out _))
        {
            if (HomeDomain.TryPickT0(out var targetHomeDomain, out _))
            {
                if (existingHomeDomain != targetHomeDomain.Value)
                {
                    var setHomeDomain = new TransactionBuilder(Account.AsT0);
                    setHomeDomain.AddOperation(new SetOptionsOperation(Account.AsT0.KeyPair).SetHomeDomain(targetHomeDomain.Value));

                    var result = await SubmitTransactionAsync(setHomeDomain.Build(), true, $"set home domain for {Account.AsT0.AccountId}");
                    if (result?.IsSuccess is null or false)
                    {
                        throw new Exception($"failed to set home domain for {Account.AsT0.AccountId}");
                    }
                }
            }
        }

        IsInitialized = true;

        return accountId;
    }

    public Task<string> InitializeAsync(string accountId) => InitializeInternalAsync(accountId);

    public async Task<string> InitializeAsync()
    {
        if (!CanUseCryptography())
        {
            throw new Exception($"This platform does not support cryptography eg 'NSec.Cryptography.Sodium.InitializeCore', please use {nameof(InitializeAsync)}(string) which does not use cryptography functions.");
        }

        return await InitializeInternalAsync(string.Empty);
    }

    public static Task CreateAccountAsync(StellarDotnetSdk.Accounts.KeyPair account, string networkUrl, HttpClient? httpClient = null)
        => CreateAccountAsync(account.Address, networkUrl, httpClient);

    public static async Task CreateAccountAsync(string account, string networkUrl, HttpClient? httpClient = null)
    {
        var disposeHttpClient = false;

        if (httpClient == null)
        {
            httpClient = new HttpClient();
            disposeHttpClient = true;
        }

        using var server = new Server(networkUrl, httpClient);

        try
        {
            // do nothing if account already exists
            _ = await server.Accounts.Account(account);
        }
        catch (Exception)
        {
            // exception thrown so account must not exist

            // if we don't assign to a value, we will not wait for the result.
            var discard = await httpClient.GetAsync($"{(networkUrl == PublicNetworkUrl.Testnet ? "https://friendbot.stellar.org" : $"{networkUrl}/friendbot")}?addr={account}");
        }

        if (disposeHttpClient)
        {
            httpClient.Dispose();
        }
    }

    public static double GetMinimumBalance(double baseReserve, int numEntries, int numSponsoringEntries, int numSponsoredEntries)
        => (2 + numEntries + numSponsoringEntries - numSponsoredEntries) * baseReserve;

    /// <summary>
    ///     Gets Stellar assets issued by an individual or organization, as defined by <paramref name="homeDomain"/>/.well-known/stellar.toml
    /// </summary>
    /// <param name="homeDomain">The domain of a Stellar asset issuer</param>
    /// <returns></returns>
    public static async Task<List<(string Name, AssetTypeCreditAlphaNum Asset, string Description, string ImageUri)>> GetRivalCoinsAsync(string homeDomain)
    {
        //using var http = new HttpClient();
        var rivalCoins = new List<(string Name, AssetTypeCreditAlphaNum Asset, string Description, string ImageUri)>();
        using var httpClient = new HttpClient();
        using var reader = new StringReader(await httpClient.GetStringAsync($"{homeDomain}/.well-known/stellar.toml"));
        var table = TOML.Parse(reader);

        foreach (TomlNode currency in table["CURRENCIES"])
        {
            var rivalCoin = (
                currency["name"],
                Asset.CreateNonNativeAsset(currency["code"], currency["issuer"]),
                currency["desc"],
                currency["image"]);

            rivalCoins.Add(rivalCoin);
        }

        return rivalCoins;
    }

    private static bool CanUseCryptography()
    {
        var canUseCryptography = false;

        try
        {
            _ = StellarDotnetSdk.Accounts.KeyPair.Random();
            canUseCryptography = true;
        }
        catch (Exception) { }

        return canUseCryptography;
    }
}