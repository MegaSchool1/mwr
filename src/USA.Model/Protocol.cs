using OneOf;
using OneOf.Types;
using Stellar;
using StellarDotnetSdk.Accounts;
using StellarDotnetSdk.Transactions;
using StellarDotnetSdk.Assets;
using StellarDotnetSdk.Memos;
using StellarDotnetSdk.Operations;

namespace USA.Model;

public static class Protocol
{
    private static readonly TimeZoneInfo NewYorkTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");

    private static async Task<StellarKeyPair> CreateRandomKeyPairAsync(OneOf<Func<Task<string>>, None> keyPairGenerator)
    {
        if(keyPairGenerator.TryPickT0(out var keyPairGetter, out _))
        {
            return await Util.CreateKeyPairRandomAsync(keyPairGetter);
        }
        else
        {
            return KeyPair.Random();
        }
    }

    public static async Task RunAsync()
    {
        //TODO Add memos

        var networkUrl = Wallet.PublicNetworkUrl.Testnet;
        var issuingAccountDomain = HomeDomain.From("megaschool1.me");
        var distributionAccountDomain = HomeDomain.From("thirdoption.party");
        var serviceFeeAccountHomeDomain = HomeDomain.From("thirdoption.party");
        var bootstrapSeed = KeyPair.Random().SecretSeed!;
        var serviceFeeAccountSeed = KeyPair.Random().SecretSeed!;
        var founderFeeAccountSeed = KeyPair.Random().SecretSeed!;

        var bootstrap = new Wallet(
            networkUrl,
            bootstrapSeed,
            new None());

        await Wallet.CreateAccountAsync(
            KeyPair.FromSecretSeed(bootstrapSeed),
            bootstrap.NetworkUrl);
        await bootstrap.InitializeAsync();
        Console.WriteLine($"Bootstrap: {bootstrap.Account.AsT0.KeyPair.AccountId}:{bootstrapSeed}");

        var currency = await Util.CreateCurrencySystemAsync(
            "USA2025",
            100L * 1000 * 1000 * 1000 * 1000,
            Util.MaxTrustlineLimit,
            issuingAccountDomain,
            distributionAccountDomain,
            bootstrap);

        var usa = Asset.CreateNonNativeAsset("USA2025", currency.Issuing.AccountId);

        var serviceFee = new Wallet(
            networkUrl,
            serviceFeeAccountSeed!,
            serviceFeeAccountHomeDomain);
        await Wallet.CreateAccountAsync(KeyPair.FromSecretSeed(serviceFee.AccountSecretSeed.AsT0), serviceFee.NetworkUrl);
        await serviceFee.InitializeAsync();
        Console.WriteLine($"Service Fee: {serviceFee.Account.AsT0.KeyPair.AccountId}:{serviceFeeAccountSeed}");

        var distributor = new Wallet(networkUrl, currency.Distributions.First().SecretSeed!, new None());
        await distributor.InitializeAsync();

        var tx = new TransactionBuilder(distributor.Account.AsT0);
        Util.SendPayment(
            usa,
            (100L * 1000 * 1000 * 1000).ToString(),
            distributor.Account.AsT0.KeyPair,
            serviceFee.Account.AsT0.KeyPair,
            tx);

        var txSigned = tx.Build();
        txSigned.Sign(distributor.AccountSecretSeed.MapT0(seed => KeyPair.FromSecretSeed(seed)).AsT0, distributor.NetworkInfo.AsT0);
        txSigned.Sign(serviceFee.AccountSecretSeed.MapT0(seed => KeyPair.FromSecretSeed(seed)).AsT0, serviceFee.NetworkInfo.AsT0);

        await serviceFee.SubmitTransactionAsync(txSigned, false, "Service Fee");

        // send founder's fee
        var founder = new Wallet(
            networkUrl,
            founderFeeAccountSeed,
            new None());
        await Wallet.CreateAccountAsync(KeyPair.FromSecretSeed(founder.AccountSecretSeed.AsT0), founder.NetworkUrl);
        await founder.InitializeAsync();
        Console.WriteLine($"Founder Fee: {founder.Account.AsT0.KeyPair.AccountId}:{founderFeeAccountSeed}");

        var founderFeeTx = new TransactionBuilder(serviceFee.Account.AsT0);
        Util.CreateTrustline(
            usa,
            founder.Account.AsT0.KeyPair,
            founderFeeTx);

        Util.SendPayment(
            usa,
            (10L * 1000 * 1000).ToString(),
            serviceFee.Account.AsT0.KeyPair,
            founder.Account.AsT0.KeyPair,
            founderFeeTx);

        var founderFeeTxSigned = founderFeeTx.Build();
        founderFeeTxSigned.Sign(serviceFee.AccountSecretSeed.MapT0(seed => KeyPair.FromSecretSeed(seed)).AsT0, serviceFee.NetworkInfo.AsT0);
        founderFeeTxSigned.Sign(founder.AccountSecretSeed.MapT0(seed => KeyPair.FromSecretSeed(seed)).AsT0, founder.NetworkInfo.AsT0);

        await serviceFee.SubmitTransactionAsync(founderFeeTxSigned, false, "Founder Fee");
    }

    private static KeyPair Signer(OneOf<string, None> seed) => KeyPair.FromSecretSeed(seed.AsT0);

    public static async Task UniversalBasicIncomeAsync()
    {
        var networkUrl = Wallet.PublicNetworkUrl.Testnet;
        (string Issuer, string Distributor) genesisSeed = (KeyPair.Random().SecretSeed!, KeyPair.Random().SecretSeed!);
        (string Alice, string Bob, string Carol) personSeed = (KeyPair.Random().SecretSeed!, KeyPair.Random().SecretSeed!, KeyPair.Random().SecretSeed!);

        if (false)
        {
            await Wallet.CreateAccountAsync(KeyPair.FromSecretSeed(personSeed.Alice).AccountId, networkUrl);
            await Wallet.CreateAccountAsync(KeyPair.FromSecretSeed(personSeed.Bob).AccountId, networkUrl);
            await Wallet.CreateAccountAsync(KeyPair.FromSecretSeed(personSeed.Carol).AccountId, networkUrl);
        }

        (Wallet Alice, Wallet Bob, Wallet Carol) person =
            (new Wallet(networkUrl, personSeed.Alice, new None()),
                new Wallet(networkUrl, personSeed.Bob, new None()),
                new Wallet(networkUrl, personSeed.Carol, new None()));
        await person.Alice.InitializeAsync();
        await person.Bob.InitializeAsync();
        await person.Carol.InitializeAsync();

        Console.WriteLine($"Alice: {person.Alice.Account.AsT0.KeyPair.AccountId}:{person.Alice.AccountSecretSeed.AsT0}");
        Console.WriteLine($"Bob: {person.Bob.Account.AsT0.KeyPair.AccountId}:{person.Bob.AccountSecretSeed.AsT0}");
        Console.WriteLine($"Carol: {person.Carol.Account.AsT0.KeyPair.AccountId}:{person.Carol.AccountSecretSeed.AsT0}");

        if (false)
        {
            await Wallet.CreateAccountAsync(KeyPair.FromSecretSeed(genesisSeed.Issuer).AccountId, networkUrl);
            await Wallet.CreateAccountAsync(KeyPair.FromSecretSeed(genesisSeed.Distributor).AccountId, networkUrl);
        }

        (Wallet Issuer, Wallet Distributor) genesis = (new Wallet(networkUrl, genesisSeed.Issuer, HomeDomain.From("megaschool1.me")), new Wallet(networkUrl, genesisSeed.Distributor, HomeDomain.From("thirdoption.party")));
        await genesis.Issuer.InitializeAsync();
        await genesis.Distributor.InitializeAsync();

        var genesisToken = Asset.CreateNonNativeAsset("Genesis", genesis.Issuer.Account.AsT0.AccountId);

        if (false)
        {
            var createGenesisToken = new TransactionBuilder(genesis.Issuer.Account.AsT0)
                .AddMemo(new MemoText("USA Genesis Token"))
                .AddOperation(new ChangeTrustOperation(genesisToken, "0.0000001", genesis.Distributor.Account.AsT0.KeyPair))
                .AddOperation(new PaymentOperation(
                    genesis.Distributor.Account.AsT0.KeyPair,
                    genesisToken,
                    "0.0000001",
                    genesis.Issuer.Account.AsT0.KeyPair))
                .AddOperation(
                    new SetOptionsOperation(genesis.Issuer.Account.AsT0.KeyPair)
                        .SetMasterKeyWeight(1)
                        .SetLowThreshold(2)
                        .SetMediumThreshold(2)
                        .SetHighThreshold(2)
                        .SetSetFlags((int)AccountFlag.AUTH_IMMUTABLE_FLAG))
                .AddOperation(
                    new SetOptionsOperation(genesis.Distributor.Account.AsT0.KeyPair)
                        .SetMasterKeyWeight(1)
                        .SetLowThreshold(2)
                        .SetMediumThreshold(2)
                        .SetHighThreshold(2)
                        .SetSetFlags((int)AccountFlag.AUTH_IMMUTABLE_FLAG))
                .Build();

            createGenesisToken.Sign(Signer(genesis.Issuer.AccountSecretSeed), genesis.Issuer.NetworkInfo.AsT0);
            createGenesisToken.Sign(Signer(genesis.Distributor.AccountSecretSeed), genesis.Distributor.NetworkInfo.AsT0);

            await genesis.Issuer.SubmitTransactionAsync(createGenesisToken, false, "USA Genesis Token");
        }

        var usa = Asset.CreateNonNativeAsset("USA2025", genesis.Issuer.Account.AsT0.AccountId);

        var lastUniversalBasicIncomeDistribution = new DateTimeOffset(2029, 1, 20, 11, 59, 59, NewYorkTimeZone.BaseUtcOffset);
        var currentTime = DateTimeOffset.UtcNow;
        var firstUniversalBasicIncomeDistribution = new DateTimeOffset(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, currentTime.Minute, currentTime.Second, currentTime.Offset);
        var universalBasicIncomeDistributionEpoch = lastUniversalBasicIncomeDistribution - firstUniversalBasicIncomeDistribution;

        var aliceBootstrap = new TransactionBuilder(person.Alice.Account.AsT0)
            .AddOperation(new ManageDataOperation("Annual Universal Basic Income", "50,000 USA", person.Alice.Account.AsT0.KeyPair))
            .AddOperation(new ChangeTrustOperation(usa, null, person.Alice.Account.AsT0.KeyPair))
            .AddOperation(new PaymentOperation(
                person.Alice.Account.AsT0.KeyPair,
                usa,
                "300000",
                genesis.Issuer.Account.AsT0.KeyPair))
            .AddTimeBounds(new(firstUniversalBasicIncomeDistribution, lastUniversalBasicIncomeDistribution))
            .Build()
            ;

           aliceBootstrap.Sign(Signer(person.Alice.AccountSecretSeed), person.Alice.NetworkInfo.AsT0);
           aliceBootstrap.Sign(Signer(genesis.Issuer.AccountSecretSeed), genesis.Issuer.NetworkInfo.AsT0);

           var aliceBootstrapXdr = aliceBootstrap.ToEnvelopeXdrBase64();
           Console.WriteLine($"Alice Bootstrap XDR: {aliceBootstrapXdr}");
    }
}
