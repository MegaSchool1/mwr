using Flow.Model.Expense;
using OneOf.Types;

namespace Flow.Model.PowerUp;

public class HealthSharing : PowerUp
{
    public override (PowerUpResult Result, GameState game) Activate(GameState game)
    {
        return
        (
            (
            Description.From($"Healing boost! The {(Enumerable.Any<Expense.Expense>(game.Expenses, e => e is TreasureMasterMembership) ? "Treasure Masters" : "Community Healing Shield")} will now pay for all healings."),
            new None()
            ),
            game
        );
    }
}