using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Dto;

public class VideoDto
{
    [JsonPropertyName("videoId")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("platform")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VideoPlatform Platform { get; set; }

    [JsonPropertyName("videoHash")]
    public string? Hash { get; set; }

    [JsonPropertyName("userHandle")]
    public string? UserHandle { get; set; }

    [JsonPropertyName("duration")]
    public TimeSpan? Duration { get;  set; }
}
