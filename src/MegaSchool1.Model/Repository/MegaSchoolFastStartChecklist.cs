using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Repository;

public class MegaSchoolFastStartChecklist
{
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

    [JsonPropertyName("ed3_roadmap")]
    public bool ExecutiveDirector3Roadmap { get; set; }

    [JsonPropertyName("watch_app_install_instructions")]
    public bool WatchAppInstallInstructions { get; set; }
  
    [JsonPropertyName("app_install")]
    public bool InstallApp { get; set; }

    [JsonPropertyName("signal_subscription")]
    public bool SubscribedToSignal { get; set; }
}
