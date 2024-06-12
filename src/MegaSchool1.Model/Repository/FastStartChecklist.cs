using MegaSchool1.Model.Repository;
using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Repository;

public class FastStartChecklist
{
    [JsonPropertyName("pay_card")]
    public bool PayCard { get; set; }

    [JsonPropertyName("fast_start_training")]
    public bool FastStartTraining { get; set; }

    [JsonPropertyName("draft_worksheet")]
    public bool FirstRoundDraftWorksheet { get; set; }

    [JsonPropertyName("fast_start_interview")]
    public bool FastStartInterview { get; set; }

    [JsonPropertyName("fast_start_interview_print")]
    public bool FastStartInterviewPrint { get; set; }

    [JsonPropertyName("fast_start_interview_fillout")]
    public bool FastStartInterviewFillOut { get; set; }

    [JsonPropertyName("virtual_business_launch")]
    public bool VirtualBusinessLaunch { get; set; }

    [JsonPropertyName("virtual_business_launch_date")]

    public DateTime? VirtualBusinessLaunchDate { get; set; }

    [JsonPropertyName("home_business_launch")]
    public bool HomeBusinessLaunch { get; set; }

    [JsonPropertyName("home_business_launch_date")]
    public DateTime? HomeBusinessLaunchDate { get; set; }

    [JsonPropertyName("instant_pay_raise")]
    public bool InstantPayRaise { get; set; }

    [JsonPropertyName("hurdlr_setup")]
    public bool HurdlrSetup { get; set; }

    [JsonPropertyName("lower_my_bills")]
    public bool LowerMyBills { get; set; }

    [JsonPropertyName("debt_eliminator")]
    public bool DebtEliminator { get; set; }

    [JsonPropertyName("debt_interest_savings")]
    public double DebtInterestSavings { get; set; }

    [JsonPropertyName("debt_free_date")]
    public DateTime? DebtFreeDate { get; set; }

    [JsonPropertyName("millionaire_date")]
    public DateTime? MillionaireDate { get; set; }

    [JsonPropertyName("restore_credit")]
    public bool RestoreCredit { get; set; }

    [JsonPropertyName("dca")]
    public bool DiversifiedCashflowAccount { get; set; }

    [JsonPropertyName("dca_consultation_date")]
    public DateTime? DiversifiedCashflowAccountConsultationDate { get; set; }

    [JsonPropertyName("team_trainings")]
    public bool TeamTrainings { get; set; }

    [JsonPropertyName("team_training_schedule")]
    public DateTime? TeamTrainingSchedule { get; set; }

    [JsonPropertyName("corporate_event_registration")]
    public bool CorporateEventRegistration { get; set; }

    [JsonPropertyName("facebook_subscription")]
    public bool SubscribedToFacebook { get; set; }

    [JsonPropertyName("youtube_subscription")]
    public bool SubscribedToYouTube { get; set; }

    [JsonPropertyName("twitter_subscription")]
    public bool SubscribedToTwitter { get; set; }

    [JsonPropertyName("linkedin_subscription")]
    public bool SubscribedToLinkedIn { get; set; }

    [JsonPropertyName("instagram_subscription")]
    public bool SubscribedToInstagram { get; set; }

    [JsonPropertyName("testimonials_subscription")]
    public bool SubscribedToTestimonials { get; set; }

    [JsonPropertyName("headshot")]
    public bool Headshot { get; set; }

    [JsonPropertyName("installed_givbux")]
    public bool InstallGivBuxApp { get; set; }

    [JsonPropertyName("set_givbux_code")]
    public bool SetGivBuxCode { get; set; }

    [JsonPropertyName("mega_school")]
    public MegaSchoolFastStartChecklist MegaSchool { get; set; } = new();

    [JsonPropertyName("extra_digit_movement")]
    public ExtraDigitFastStartChecklist ExtraDigitMovement { get; set; } = new();

    [JsonPropertyName("ark_nation")]
    public ArkNationFastStartChecklist ArkNation { get; set; } = new();
}
