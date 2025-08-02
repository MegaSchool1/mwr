using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Dto;

public readonly record struct ShareableDto(
    [property: JsonPropertyName("contentId"), ConfigurationKeyName("contentId")]
    Content ContentId,
    
    [property: JsonPropertyName("videoId"), ConfigurationKeyName("videoId")]
    string? Id,
    
    [property: JsonPropertyName("platform"), ConfigurationKeyName("platform")]
    VideoPlatform Platform,
    
    [property: JsonPropertyName("videoHash"), ConfigurationKeyName("videoHash")]
    string? Hash,

    [property: JsonPropertyName("userHandle"), ConfigurationKeyName("userHandle")]
    string? UserHandle,
  
    [property: JsonPropertyName("url"), ConfigurationKeyName("url")]
    string? Url,
    
    [property: JsonPropertyName("appTitle"), ConfigurationKeyName("appTitle")]
    string AppTitle,
    
    [property: JsonPropertyName("shareableTitle"), ConfigurationKeyName("shareableTitle")]
    string ShareableTitle,
    
    [property: JsonPropertyName("duration"), ConfigurationKeyName("duration")]
    TimeSpan? Duration,
    
    [property: JsonPropertyName("imageId"), ConfigurationKeyName("imageId")]
    Image? Image,
    
    [property: JsonPropertyName("imageUrl"), ConfigurationKeyName("imageUrl")]
    string? ImageUrl,
    
    [property: JsonPropertyName("capturePageImageId"), ConfigurationKeyName("capturePageImageId")]
    Image? CapturePageImage,
    
    [property: JsonPropertyName("capturePageImageUrl"), ConfigurationKeyName("capturePageImageUrl")]
    string? CapturePageImageUrl,
    
    [property: JsonPropertyName("downloadText"), ConfigurationKeyName("downloadText")]
    string? DownloadText,
    
    [property: JsonPropertyName("downloadUrl"), ConfigurationKeyName("downloadUrl")]
    string? DownloadUrl,
    
    [property: JsonPropertyName("promoExpiration"), ConfigurationKeyName("promoExpiration")]
    DateTimeOffset? PromoExpiration,
    
    [property: JsonPropertyName("event"), ConfigurationKeyName("event")]
    EventDto? Event,
    
    [property: JsonPropertyName("showBusinessSignUp"), ConfigurationKeyName("showBusinessSignUp")]
    bool ShowBusinessSignUp,
    
    [property: JsonPropertyName("hideShortCodePrompt"), ConfigurationKeyName("hideShortCodePrompt")]
    bool HideShortCodePrompt,
    
    [property: JsonPropertyName("images"), ConfigurationKeyName("images")]
    ImageInfo[]? Images,
    
    [property: JsonPropertyName("auxText"), ConfigurationKeyName("auxText")]
    string? AuxiliaryText,
    
    [property: JsonPropertyName("auxTexts"), ConfigurationKeyName("auxTexts")]
    string[]? AuxiliaryTexts,
    
    [property: JsonPropertyName("alternateVideos"), ConfigurationKeyName("alternateVideos")]
    VideoDto[]? AlternateVideos,
    
    [property: JsonPropertyName("metadata"), ConfigurationKeyName("metadata")]
    string? Metadata,
    
    [property: JsonPropertyName("flyerImageUrl"), ConfigurationKeyName("flyerImageUrl")]
    string? FlyerImageUrl,
    
    [property: JsonPropertyName("className"), ConfigurationKeyName("className")]
    string? ClassName,
    
    [property: JsonPropertyName("classDescription"), ConfigurationKeyName("classDescription")]
    string? ClassDescription,
    
    [property: JsonPropertyName("start"), ConfigurationKeyName("start")]
    TimeSpan? Start,
    
    [property: JsonPropertyName("hideCapturePageVideo"), ConfigurationKeyName("hideCapturePageVideo")]
    bool HideCapturePageVideo,
    
    [property: JsonPropertyName("clipId"), ConfigurationKeyName("clipId")]
    string? ClipId,
    
    [property: JsonPropertyName("clipTimestamp"), ConfigurationKeyName("clipTimestamp")]
    string? ClipTimestamp,
    
    [property: JsonPropertyName("showHeaderImage"), ConfigurationKeyName("showHeaderImage")]
    bool ShowHeaderImage = true,

    [property: JsonPropertyName("promo"), ConfigurationKeyName("promo")]
    string? Promo  = "For $100 off, text"
)
{
    public override string ToString() => $"{ContentId}";
}
