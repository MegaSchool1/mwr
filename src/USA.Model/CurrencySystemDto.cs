namespace USA.Model;

public class CurrencySystemDto
{
    public string AssetCode { get; set; } = null!;
    public string BaseIssuingAccount { get; set; } = null!;
    public string[] BaseDistributionAccounts { get; set; } = [];
}