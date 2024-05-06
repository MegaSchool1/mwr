using MegaSchool1.Model;

namespace MegaSchool1.ViewModel;

public class ExtraDigitMovementPipeline : Pipeline<Model.ExtraDigitMovementPipeline>
{
	public override Strategy Strategy => Strategy.ExtraDigitMovement;

	public override string DisplayName(Model.ExtraDigitMovementPipeline step) => step switch
    {
        Model.ExtraDigitMovementPipeline.Enqueue => "Enqueue",
        Model.ExtraDigitMovementPipeline.Question => "Question + 14min",
        Model.ExtraDigitMovementPipeline.Connection => "Text Connection (Name?)",
        Model.ExtraDigitMovementPipeline.Invitation => "Invitation (What Event?)",
        Model.ExtraDigitMovementPipeline.Decision => "Decision? (Out, MBR, QMD)",
        _ => throw new NotImplementedException($"'{step}' has no display name defined")
    };
}
