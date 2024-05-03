using System.Text.Json.Serialization;

namespace MegaSchool1.Repository.Model;

public class ExtraDigitFastStartChecklist
{
    [JsonPropertyName("facebook_page_subscription")]
    public bool SubscribedToFacebookPage { get; set; }
    
    [JsonPropertyName("facebook_group_subscription")]
    public bool SubscribedToFacebookGroup { get; set; }

    [JsonPropertyName("youtube_subscription")]
    public bool SubscribedToYouTube { get; set; }

    [JsonPropertyName("personal_activity_report")]
    public bool PersonalActivityReport { get; set; }
}
