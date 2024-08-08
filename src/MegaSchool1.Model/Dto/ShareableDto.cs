using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Dto;

public class ShareableDto
{
    [JsonPropertyName("contentId")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Content ContentId { get; set; }

    [JsonPropertyName("videoId")]
    public string? Id { get; set; }

    [JsonPropertyName("platform")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VideoPlatform Platform { get; set; }

    [JsonPropertyName("videoHash")]
    public string? Hash { get; set; }

    [JsonPropertyName("userHandle")]
    public string? UserHandle { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("appTitle")]
    public string AppTitle { get; set; } = default!;

    [JsonPropertyName("shareableTitle")]
    public string ShareableTitle { get; set; } = default!;
    
    [JsonPropertyName("duration")]
    public TimeSpan? Duration { get;  set; }

    [JsonPropertyName("imageId")]
    public Image? Image { get; set; }

    [JsonPropertyName("capturePageImageId")]
    public Image? CapturePageImage { get; set; }

    [JsonPropertyName("downloadText")]
    public string? DownloadText { get; set; }

    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; set; }

    [JsonPropertyName("promo")]
    public string? Promo { get; set; }

    [JsonPropertyName("promoExpiration")]
    public DateTimeOffset? PromoExpiration { get; set; }

    [JsonPropertyName("event")]
    public EventDto? Event { get; set; }

    [JsonPropertyName("showBusinessSignUp")]
    public bool ShowBusinessSignUp { get; set; }

    [JsonPropertyName("hideShortCodePrompt")]
    public bool HideShortCodePrompt { get; set; }

    [JsonPropertyName("images")]
    public ImageInfo[] Images { get; set; } = [];

    [JsonPropertyName("auxText")]
    public string? AuxiliaryText { get; set; }

    [JsonPropertyName("alternateVideos")]
    public VideoDto[] AlternateVideos { get; set; } = [];
}
