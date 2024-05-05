namespace MegaSchool1.ViewModel;

public class MegaSchoolPipeline : Pipeline<Model.MegaSchoolPipeline>
{
    public override string DisplayName(Model.MegaSchoolPipeline step) => step switch
    {
        Model.MegaSchoolPipeline.Enqueue => "Enqueue",
        Model.MegaSchoolPipeline.ListenAndAsk => "Listen & Ask",
        Model.MegaSchoolPipeline.Share => "Share",
        Model.MegaSchoolPipeline.Connect => "Connect",
        Model.MegaSchoolPipeline.Decision => "Decision",
        _ => throw new NotImplementedException($"'{step}' has no display name defined")
    };
}
