using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Dto;

public record ShareableDto
{
    [JsonPropertyName("contentId")]
    [ConfigurationKeyName("contentId")]
    public Content ContentId { get; init; }

    [JsonPropertyName("videoId")]
    [ConfigurationKeyName("videoId")]
    public string? Id { get; init; }

    [JsonPropertyName("platform")]
    public VideoPlatform Platform { get; init; }

    [JsonPropertyName("videoHash")]
    [ConfigurationKeyName("videoHash")]
    public string? Hash { get; init; }

    [JsonPropertyName("userHandle")]
    public string? UserHandle { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }

    [JsonPropertyName("appTitle")]
    public string AppTitle { get; init; } = default!;

    [JsonPropertyName("shareableTitle")]
    public string ShareableTitle { get; init; } = default!;
    
    [JsonPropertyName("duration")]
    public TimeSpan? Duration { get;  init; }

    [JsonPropertyName("imageId")]
    [ConfigurationKeyName("imageId")]
    public Image? Image { get; init; }

    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; init; }
    
    [JsonPropertyName("showHeaderImage")]
    public bool ShowHeaderImage { get; init; } = true;

    [JsonPropertyName("capturePageImageId")]
    [ConfigurationKeyName("capturePageImageId")]
    public Image? CapturePageImage { get; init; }

    [JsonPropertyName("capturePageImageUrl")]
    [ConfigurationKeyName("capturePageImageUrl")]
    public string? CapturePageImageUrl { get; init; }
    
    [JsonPropertyName("downloadText")]
    public string? DownloadText { get; init; }

    [JsonPropertyName("downloadUrl")]
    public string? DownloadUrl { get; init; }

    [JsonPropertyName("promo")]
    public string? Promo { get; init; } = "For $100 off, text";

    [JsonPropertyName("promoExpiration")]
    public DateTimeOffset? PromoExpiration { get; init; }

    [JsonPropertyName("event")]
    public EventDto? Event { get; init; }

    [JsonPropertyName("showBusinessSignUp")]
    public bool ShowBusinessSignUp { get; init; }

    [JsonPropertyName("hideShortCodePrompt")]
    public bool HideShortCodePrompt { get; init; }

    [JsonPropertyName("images")]
    public ImageInfo[] Images { get; init; } = [];

    [JsonPropertyName("auxText")]
    [ConfigurationKeyName("auxText")]
    public string? AuxiliaryText { get; init; }
    
    [JsonPropertyName("auxTexts")]
    [ConfigurationKeyName("auxTexts")]
    public string[] AuxiliaryTexts { get; init; } = [];

    [JsonPropertyName("alternateVideos")]
    public VideoDto[] AlternateVideos { get; init; } = [];

    [JsonPropertyName("metadata")]
    public string? Metadata { get; init; }

    [JsonPropertyName("flyerImageUrl")]
    public string? FlyerImageUrl { get; init; }

    [JsonPropertyName("className")]
    public string? ClassName { get; init; }

    [JsonPropertyName("classDescription")]
    public string? ClassDescription { get; init; }

    [JsonPropertyName("start")]
    public TimeSpan? Start { get;  init; }
    
    [JsonPropertyName("hideCapturePageVideo")]
    [ConfigurationKeyName("hideCapturePageVideo")]
    public bool HideCapturePageVideo { get;  init; }
    
    [JsonPropertyName("clipId")]
    [ConfigurationKeyName("clipId")]
    public string? ClipId { get; init; }
    
    [JsonPropertyName("clipTimestamp")]
    [ConfigurationKeyName("clipTimestamp")]
    public string? ClipTimestamp { get; init; }
    
    public override string ToString() => $"{ContentId}";
}
