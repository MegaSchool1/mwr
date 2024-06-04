using System.Text.Json.Serialization;

namespace MegaSchool1.Model;

public record ImageInfo
{
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    [JsonPropertyName("href")]
    public string? Href { get; set; }
}
