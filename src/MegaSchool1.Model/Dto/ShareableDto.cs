using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Dto;

public class ShareableDto
{
    [JsonPropertyName("contentId")]
    [ConfigurationKeyName("contentId")]
    public Content ContentId { get; set; }

    [JsonPropertyName("videoId")]
    [ConfigurationKeyName("videoId")]
    public string? Id { get; set; }

    [JsonPropertyName("platform")]
    public VideoPlatform Platform { get; set; }

    [JsonPropertyName("videoHash")]
    [ConfigurationKeyName("videoHash")]
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
    [ConfigurationKeyName("imageId")]
    public Image? Image { get; set; }

    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; set; }
    
    [JsonPropertyName("showHeaderImage")]
    public bool ShowHeaderImage { get; set; } = true;

    [JsonPropertyName("capturePageImageId")]
    [ConfigurationKeyName("capturePageImageId")]
    public Image? CapturePageImage { get; set; }

    [JsonPropertyName("capturePageImageUrl")]
    [ConfigurationKeyName("capturePageImageUrl")]
    public string? CapturePageImageUrl { get; set; }
    
    [JsonPropertyName("downloadText")]
    public string? DownloadText { get; set; }

    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; set; }

    [JsonPropertyName("promo")]
    public string? Promo { get; set; } = "For $100 off, text";

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
    [ConfigurationKeyName("auxText")]
    public string? AuxiliaryText { get; set; }
    
    [JsonPropertyName("auxTexts")]
    [ConfigurationKeyName("auxTexts")]
    public string[] AuxiliaryTexts { get; set; } = [];

    [JsonPropertyName("alternateVideos")]
    public VideoDto[] AlternateVideos { get; set; } = [];

    [JsonPropertyName("metadata")]
    public string? Metadata { get; set; }

    [JsonPropertyName("flyerImageUrl")]
    public string? FlyerImageUrl { get; set; }

    [JsonPropertyName("className")]
    public string? ClassName { get; set; }

    [JsonPropertyName("classDescription")]
    public string? ClassDescription { get; set; }

    [JsonPropertyName("start")]
    public TimeSpan? Start { get;  set; }
    
    [JsonPropertyName("hideCapturePageVideo")]
    [ConfigurationKeyName("hideCapturePageVideo")]
    public bool HideCapturePageVideo { get;  set; }
    
    [JsonPropertyName("clipId")]
    [ConfigurationKeyName("clipId")]
    public string? ClipId { get; set; }
    
    [JsonPropertyName("clipTimestamp")]
    [ConfigurationKeyName("clipTimestamp")]
    public string? ClipTimestamp { get; set; }
    
    public override string ToString() => $"{ContentId}";
}
