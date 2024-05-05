using System.Text.Json.Serialization;
using MegaSchool1.Model;

namespace MegaSchool1.Repository.Model;

public class Prospect
{
	[JsonPropertyName("version")]
	public string Version { get; set; } = ProspectVersion.v1_0_0_0.ToString();

	[JsonPropertyName("Message")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("order")]
    public int Order { get; set; }
}