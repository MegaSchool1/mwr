using System.Text.Json.Serialization;

namespace MegaSchool1.Model;

public class VideoResource
{
    [JsonPropertyName("contentId")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Content ContentId { get; set; }

    [JsonPropertyName("videoId")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("platform")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VideoPlatform Platform { get; set; }

    [JsonPropertyName("videoHash")]
    public string? Hash { get; set; }

    [JsonPropertyName("userHandle")]
    public string? UserHandle { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
