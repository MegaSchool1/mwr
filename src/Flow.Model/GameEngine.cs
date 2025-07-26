using Flow.Model.Expense;
using Flow.Model.PowerUp;
using Foundation.Model;
using OneOf;
using OneOf.Types;
using ValueOf;

namespace Flow.Model;

public static class GameEngine
{
    public static (OneOf<Description[], Error<string>> Result, GameState Game) ProcessPowerUpsForCurrentDay(GameState game)
    {
        var processedPowerUps = new List<Description>();

        foreach (var powerUp in game.PowerUps.Where( p => p is not BillNegotiator))
        {
            var powerUpResult = powerUp.Activate(game);
            game = powerUpResult.game;

            if (powerUpResult.Result.TryPickT0(out var description, out var error))
            {
                processedPowerUps.Add(description.Description);
            }
            else
            {
                return (error, game);
            }
        }

        return (processedPowerUps.ToArray(), game);
    }

    public static (Expense.Expense[] Expenses, GameState Game) ProcessExpensesForCurrentDay(GameState game)
    {
        var processedExpenses = new List<Expense.Expense>();

        if(!game.CurrentDayStats.ProcessedExpenses)
        {
            foreach (var expense in game.Expenses.Where(e => e.IsDueOn(game.Day)))
            {
                game = game with { CheckingAccountBalance = game.CheckingAccountBalance - expense.Amount };
                processedExpenses.Add(expense);
            }
        }

        game.CurrentDayStats.Expenses.AddRange(processedExpenses);
        game.CurrentDayStats.ProcessedExpenses = true;

        return (processedExpenses.ToArray(), game);
    }

    private static ((decimal Gross, decimal Net, Income Income)[] Incomes, GameState Game) EarnW2IncomeForCurrentDay(GameState game)
    {
        var dailyIncomes = game.Incomes.OfType<W2Income>()
            .Where(i => i.PayFrequency == TimeSpan.FromDays(1))
            .Select(income => (Gross: income.GetGross(game.Day), Net: income.Net(game.Day), Income: (Income)income)).ToArray();
        var dailyIncome = (Gross: dailyIncomes.Sum(i => i.Gross), Net: dailyIncomes.Sum(i => i.Net));

        game.CurrentDayStats.Income = dailyIncome;
        game.CurrentDayStats.Incomes = dailyIncomes;

        // thefts
        foreach(var income in dailyIncomes.Select(i => i.Income))
        {
            var thefts = income.Thefts
                .Select(t => (t.Name, Amount: t.StolenOn(income, game.Day)))
                .Where(t => t.Amount.IsT0)
                .Select(t => (t.Name, Amount: t.Amount.AsT0));

            game.CurrentDayStats.Thefts.AddRange(thefts);
        }

        game = game with { SuccessiveWorkDays = game.SuccessiveWorkDays + 1 };

        return (dailyIncomes, game with { CheckingAccountBalance = game.CheckingAccountBalance + dailyIncome.Net });
    }

    public static (OneOf<PowerUpResult, None> Result, GameState Game) InstantPayRaise(GameState game)
    {
        if (!game.PowerUps.Any(p => p is InstantPayRaise))
        {
            var instantPayRaise = new InstantPayRaise();

            game.PowerUps.Add(instantPayRaise);

            return instantPayRaise.Activate(game);
        }

        return (new None(), game);
    }

    public static (OneOf<PowerUpResult, None> Result, GameState Game) SummonBillNegotiator(GameState game)
    {
        if (!game.PowerUps.Any(p => p is BillNegotiator))
        {
            var negotiator = new BillNegotiator();

            var result = negotiator.Activate(game);

            result.game.PowerUps.Add(negotiator);

            return result;
        }

        return (new None(), game);
    }

    public static (OneOf<MonthlySavings, None> Savings, GameState Game) SummonTreasureMasters(GameState game)
    {
        var treasureMastersMembership = new TreasureMasterMembership(game.Day);
        game.Expenses.Add(treasureMastersMembership);

        game = game with { CheckingAccountBalance = game.CheckingAccountBalance - treasureMastersMembership.Amount };

        var monthlySavings = 0.0m;

        // instant pay raise
        var instantPayRaise = InstantPayRaise(game);
        monthlySavings += instantPayRaise.Result.Match(
            powerUpResult =>
                powerUpResult.Match(
                    result => result.Savings.Match(
                        savings => savings.Value,
                        none => 0.0m),
                    error => 0.0m),
            none => 0.0m);

        // bill negotiator
        var billNegotiator = SummonBillNegotiator(game);
        monthlySavings += billNegotiator.Result.Match(
            powerUpResult =>
                powerUpResult.Match(
                    result => result.Savings.Match(
                        savings => savings.Value,
                        none => 0.0m),
                    error => 0.0m),
            none => 0.0m);
    
        // health sharing
        var healthSharing = SummonHealthSharing(game);
        monthlySavings += healthSharing.Result.Match(
            powerUpResult =>
                powerUpResult.Match(
                    result => result.Savings.Match(
                        savings => savings.Value,
                        none => 0.0m),
                    error => 0.0m),
            none => 0.0m);

        if (monthlySavings > treasureMastersMembership.Amount)
        {
            return (MonthlySavings.From(monthlySavings - treasureMastersMembership.Amount), game);
        }
        else
        {
            game = game with { CheckingAccountBalance = game.CheckingAccountBalance + treasureMastersMembership.Amount };
            return (new None(), game);
        }
    }

    public static (OneOf<PowerUpResult, None> Result, GameState Game) SummonHealthSharing(GameState game)
    {
        if (!game.Expenses.Any(e => e is HealthSharingContribution or TreasureMasterMembership))
        {
            var healthSharingExpense = new HealthSharingContribution(game.Day);

            game.Expenses.Add(healthSharingExpense);

            game = game with { CheckingAccountBalance = game.CheckingAccountBalance - healthSharingExpense.Amount };

            return (new None(), game);
        }

        return (new None(), game);
    }

    public static ((decimal Gross, decimal Net, Income Income)[] Incomes, GameState Game) EarnIncomeForCurrentDay(GameState game)
    {
        const int HealthySuccessiveWorkDays = 6;

        (decimal Gross, decimal Net, Income Income)[] dailyIncomes = [];
        var numUnhealthyWorkDays = (game.SuccessiveWorkDays + 1) - HealthySuccessiveWorkDays;

        if (numUnhealthyWorkDays <= 0)
        {
            var dailyIncomeResult = EarnW2IncomeForCurrentDay(game);

            dailyIncomes = dailyIncomeResult.Incomes;

            game = dailyIncomeResult.Game with { SickDayLikelihood = new Percentage(0, DecimalFormat.Instance) };
        }
        else
        {
            const int HealthLikelihoodDecrementPercentage = 5;
            const int MaxHealthLikelihoodDecreasePercentage = 95;
            const int MaxDecreasingHealthWorkDays = MaxHealthLikelihoodDecreasePercentage / HealthLikelihoodDecrementPercentage;

            var sickDayLikelihood = Math.Min(numUnhealthyWorkDays, MaxDecreasingHealthWorkDays) * HealthLikelihoodDecrementPercentage;
            var sickness = Random.Shared.Next(0, 100);
            if (sickDayLikelihood > sickness)
            {
                game = game with { SuccessiveWorkDays = 0 };
            }
            else
            {
                var dailyIncomeResult = EarnW2IncomeForCurrentDay(game);

                dailyIncomes = dailyIncomeResult.Incomes;
                game = dailyIncomeResult.Game;
            }

            game = game with { SickDayLikelihood = new Percentage(sickDayLikelihood, PercentageFormat.Instance) };
        }

        game.CurrentDayStats.CompletedActiveIncomeWork = true;

        return (dailyIncomes, game);
    }

    public static SpinResult Sleep(GameState game) => Instant(new() { GoToWork = false }, game);

    public static SpinResult Instant(SpinChoices choices, GameState game)
    {
        var errors = new List<Error<string>>();
        (decimal Gross, decimal Net, Income Income)[] dailyIncomes = [];

        if (choices.GoToWork)
        {
            (dailyIncomes, game) = EarnIncomeForCurrentDay(game);
        }

        var wentToWorkToday = game.CurrentDayStats.Incomes.Any();
        game = game with
        {
            SuccessiveWorkDays = wentToWorkToday ? game.SuccessiveWorkDays : 0,
            SickDayLikelihood = wentToWorkToday ? game.SickDayLikelihood : new Percentage(0, DecimalFormat.Instance)
        };

        // expense
        var expenseReport = ProcessExpensesForCurrentDay(game);
        game = expenseReport.Game;

        // power ups
        var powerUpResults = ProcessPowerUpsForCurrentDay(game);
        game = powerUpResults.Game;
        if (powerUpResults.Result.TryPickT1(out var error, out var powerUps))
        {
            errors.Add(error);
        }

        // advance to next day
        game = game.GoToNextDay();

        return new(((decimal Gross, decimal Net, Income Income)[])dailyIncomes, (Expense.Expense[])expenseReport.Expenses, (Description[])(powerUpResults.Result.IsT0 ? powerUps : []), errors.ToArray(), (GameState)game);
    }

    public class MonthlySavings : ValueOf<decimal, MonthlySavings>;
}