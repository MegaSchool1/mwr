using System.Text.Json.Serialization;

namespace MegaSchool1.Model;

public class ClientSettings
{
    [JsonPropertyName("latestVersion")]
    public string LatestVersion { get; set; } = default!;

    [JsonPropertyName("Settings")]
    public UISettings UI { get; set; } = default!;
}