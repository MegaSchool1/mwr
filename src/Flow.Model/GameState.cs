using System.Collections.ObjectModel;
using Flow.Model.Expense;
using Foundation.Model;
using OneOf;
using OneOf.Types;

namespace Flow.Model;

public record GameState(
    decimal CheckingAccountBalance,
    decimal SavingsAccountBalance,
    int SuccessiveWorkDays,
    Percentage SickDayLikelihood,
    DayOfYear Day,
    OneOf<TimeSpan, None> DialogAutoClose)
{
    public static readonly int DaysInMonth = 28;
    public static readonly YearalMonth[] Months = Enum.GetValues<YearalMonth>();
    public static readonly TimeSpan BoardEpoch = TimeSpan.FromDays(DaysInMonth);

    public (DayStats[] DayOfMonth, DayStats YearDay) Days { get; private set; } = GetClearBoard(YearalMonth.January);

    private Dictionary<DayOfYear, (PowerUp.PowerUp PowerUp, decimal Savings)> _savings = [];
    public ReadOnlyDictionary<DayOfYear, (PowerUp.PowerUp PowerUp, decimal Savings)> Savings => _savings.AsReadOnly();

    public List<Income> Incomes { get; } = [];
    public List<Expense.Expense> Expenses { get; } = [];
    public List<Loan> Debts { get; } = [];
    public List<PowerUp.PowerUp> PowerUps { get; } = [];
    public DayStats CurrentDayStats => GetDayStats(this.Day);

    public DayStats GetDayStats(DayOfYear day) => day.Match(dayOfMonth => Days.DayOfMonth[dayOfMonth.DayOfMonth - 1], yearDay => Days.YearDay);

    private static (DayStats[] DayOfMonth, DayStats YearDay) GetClearBoard(YearalMonth month) => (Enumerable.Range(1, BoardEpoch.Days).Select(i => new DayStats((month, i))).ToArray(), new DayStats(YearDay.Instance));

    public GameState GoToNextDay()
    {
        // reset board if Year Day or last day of month
        if (Day.TryPickT1(out var yearDay, out var dayOfMonth) || dayOfMonth.DayOfMonth == 28)
        {
            this.Days = GetClearBoard(YearalMonth.January);
        }

        var updatedGame = this with { Day = Day.AddDays(1) };
        updatedGame = GameEngine.ProcessExpensesForCurrentDay(updatedGame).Game;

        return updatedGame;
    }

    public static GameState Moderate()
    {
        const int AverageAmericanAnnualSalary = 60 * 1000;

        var payFrequency = TimeSpan.FromDays(1);
        var primaryIncome = new W2Income(AverageAmericanAnnualSalary / (int)(TimeSpan.FromDays(365) / payFrequency), payFrequency);

        var game = new GameState(
            // 2 weeks of salary ready to spend
            primaryIncome.GrossDuring(TimeSpan.FromDays(14)),

             // 3 month emergency fund
            primaryIncome.GrossDuring(3 * BoardEpoch),
            0,
            new Percentage(0, DecimalFormat.Instance),
            (YearalMonth.January, 1),
            new None()
        );

        game.Incomes.Add(primaryIncome);

        // expenses
        game.Expenses.Add(new((decimal)1000, (OneOf<TimeSpan, OneTime>)BoardEpoch, (DayOfYear)new ((YearalMonth.January, Random.Shared.Next(1, BoardEpoch.Days))), (string)"Rent"));
        game.Expenses.Add(new((decimal)200, (OneOf<TimeSpan, OneTime>)BoardEpoch, (DayOfYear)new((YearalMonth.January, Random.Shared.Next(1, BoardEpoch.Days))), (string)"Health Insurance"));
        game.Expenses.Add(new CellPhone());

        // debts
        {
            // credit card
            var balance = 2000.0m;
            var apy = new Percentage(0.22m, DecimalFormat.Instance);
            game.Debts.Add(new((string)"Credit Card", balance, (Percentage)apy,
                (OneOf<decimal, None>)(balance * apy.ToFormat(DecimalFormat.Instance) / Enum.GetValues<YearalMonth>().Length)));
        }

        {
            // student loan
            var balance = 10000.0m;
            var apy = new Percentage(0.055m, DecimalFormat.Instance);
            game.Debts.Add(new((string)"Student Loan", balance, (Percentage)apy, (OneOf<decimal, None>)(balance * apy.ToFormat(DecimalFormat.Instance) / Enum.GetValues<YearalMonth>().Length)));
        }

        // proces expenses
        game = GameEngine.ProcessExpensesForCurrentDay(game).Game;

        return game;
    }
}
