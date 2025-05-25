using System.Transactions;
using Flow.Model;
using FluentAssertions;
using FsCheck;
using FsCheck.Fluent;
using NUnit.Framework;

namespace MegaSchool1.Model.Test.Game;

[TestFixture]
public class GameEngineTests
{
    private static Gen<GameState> ModerateGame() => Gen.Fresh(() => GameState.Moderate());

    private static GameState InstantPayRaiseSummon(GameState g) => GameEngine.InstantPayRaise(g).Game;

    private static GameState BillNegotiatorSummon(GameState g) => GameEngine.SummonBillNegotiator(g).Game;

    private static GameState HealthSharing(GameState game) => GameEngine.SummonHealthSharing(game).Game;

    private static GameState Spin(GameState game) => GameEngine.Instant(new() { GoToWork = true }, game).Game;

    private static Gen<Func<GameState, GameState>[]> IndividualWealthMembershipServices(params Func<GameState, GameState>[] exclude) =>
        Gen.Shuffle([
            InstantPayRaiseSummon,
            BillNegotiatorSummon,
            HealthSharing
        ])
        .Select(services => services.Except(exclude).ToArray());

    private static Gen<Func<GameState, GameState>> RandomWealthMembershipService(params Func<GameState, GameState>[] exclude) =>
        IndividualWealthMembershipServices(exclude).Select(services => services.First());

    [FsCheck.NUnit.Property]
    public Property WealthMembership()
    {
       return Prop.ForAll(ModerateGame().ToArbitrary(), RandomWealthMembershipService().ToArbitrary(),
            (game, addServiceFunc) =>
            {
                GameEngine.SummonTreasureMasters(game);
                var checkingBefore = game.CheckingAccountBalance;

                var actual = addServiceFunc(game);

                actual.CheckingAccountBalance
                    .Should().Be(checkingBefore);
            });
    }

    [FsCheck.NUnit.Property]
    public Property IndividualFinancialService()
    {
        return Prop.ForAll(ModerateGame().ToArbitrary(), RandomWealthMembershipService(InstantPayRaiseSummon, BillNegotiatorSummon).ToArbitrary(),
            (game, addServiceFunc) =>
            {
                var checkingBefore = game.CheckingAccountBalance;

                var actual = addServiceFunc(game);
                
                actual.CheckingAccountBalance
                    .Should().BeLessThan(checkingBefore,  $"{addServiceFunc.Method}");
            });
    }

    [FsCheck.NUnit.Property]
    public Property MoneyBackGuarantee()
    {
        return Prop.ForAll(ModerateGame().ToArbitrary(),
            game =>
            {
                // arrange
                var checkingBefore = game.CheckingAccountBalance;

                // act
                var actual = GameEngine.SummonTreasureMasters(game);

                // assert
                if (actual.Savings.TryPickT0(out var savings, out _))
                {
                    savings.Value
                        .Should().BePositive($"{actual}");

                    actual.Game.CheckingAccountBalance
                        .Should().BeGreaterThan(checkingBefore, $"{actual}");
                }
                else
                {
                    actual.Game.CheckingAccountBalance
                        .Should().BeGreaterOrEqualTo(checkingBefore, $"{actual}");
                }
            });
    }

    [Test]
    public void CompleteMonth()
    {
        // arrange
        var game = GameState.Moderate();

        // act
        for (var i = 0; i < GameState.DaysInMonth; i++)
        {
            game = GameEngine.Instant(new() { GoToWork = true }, game).Game;
        }

        // assert
        game.Day
            .Should().Be((DayOfYear)(YearalMonth.February, 1));
    }

    [TestCase(YearalMonth.January, 1, YearalMonth.January, 2)]
    [TestCase(YearalMonth.January, 27, YearalMonth.January, 28)]
    [TestCase(YearalMonth.January, 28, YearalMonth.February, 1)]
    public void AddDay(YearalMonth currentMonth, int currentDayOfMonth, YearalMonth expectedMonth, int expectedDayOfMonth)
    {
        // arrange
        var day = new DayOfYear((currentMonth, currentDayOfMonth));

        // act
        var actual = day.AddDays(1);

        // assert
        actual
            .Should().Be((DayOfYear)(expectedMonth, expectedDayOfMonth));
    }

    [Test]
    public void AddDay_LastDayOfYear()
    {
        // arrange
        var day = new DayOfYear((YearalMonth.December, 28));

        // act
        var actual = day.AddDays(1);

        // assert
        actual
            .Should().Be((DayOfYear)YearDay.Instance);
    }

    [Test]
    public void AddDay_YearDay()
    {
        // arrange
        var day = new DayOfYear(YearDay.Instance);

        // act
        var actual = day.AddDays(1);

        // assert
        actual
            .Should().Be((DayOfYear)(YearalMonth.January, 1));
    }
}