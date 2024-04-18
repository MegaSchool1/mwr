namespace MegaSchool1.ViewModel;

public class MegaSchoolPipeline : Pipeline<Model.MegaSchoolPipeline>
{
    public override string DisplayName(Model.MegaSchoolPipeline step) => step switch
    {
        Model.MegaSchoolPipeline.Enqueue => "Enqueue",
        Model.MegaSchoolPipeline.Share => "Ask + Share",
        Model.MegaSchoolPipeline.MembershipInvitation => "Membership Invitation",
        Model.MegaSchoolPipeline.MembershipDecision => "Membership Decision",
        Model.MegaSchoolPipeline.Connection => "Upline Connection",
        Model.MegaSchoolPipeline.BusinessOpportunityInvitation => "Business Invitation",
        Model.MegaSchoolPipeline.BusinessOpportunityDecision => "Business Decision",
        _ => throw new NotImplementedException($"'{step}' has no display name defined")
    };
}
