using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Repository;

public record TeamMember
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("member_id")]
    public string MemberId { get; set; } = default!;

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
}