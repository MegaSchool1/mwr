using Foundation.Model;
using OneOf;
using OneOf.Types;

namespace Flow.Model;

public record Theft(string Name, string Description, OneOf<Percentage, (Scalar Amount, TimeSpan Frequency)> Amount)
{
    public OneOf<decimal, None> StolenOn(Income income, DayOfYear day)
    {
        var stolenAmount = 0.0m;

        if (Amount.TryPickT0(out var percentage, out var scalar))
        {
            stolenAmount = (income.Gross / income.PayFrequency.Days) * percentage.ToFormat(DecimalFormat.Instance);
        }
        else
        {
            var yearToDateInDays = day.DayNumber();
            var theftsToDate = Math.DivRem(yearToDateInDays, scalar.Frequency.Days);

            var thiefStoleToday = theftsToDate.Remainder == 0;
            if (thiefStoleToday)
            {
                stolenAmount = scalar.Amount.Value;
            }
        }

        return stolenAmount > 0.0m ? stolenAmount : new None();
    }
}

public record FederalIncomeTax() : Theft("Big Realm Levy", "United States Federal income tax", new Percentage(0.22m, DecimalFormat.Instance));
public record IllinoisIncomeTax() : Theft("Mid Realm Levy", "Illinois State Income tax", new Percentage(0.0495m, DecimalFormat.Instance));
public record SocialSecurityTax() : Theft("Common Wealth Levy", "United State Social Security Tax", new Percentage(0.062m, DecimalFormat.Instance));
public record MedicareTax() : Theft("Healing Pool Levy", "United State Medicare tax", new Percentage(0.0145m, DecimalFormat.Instance));
