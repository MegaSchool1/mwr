using MegaSchool1.Model.API;
using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Repository;

public record TeamMember
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("member_id")]
    public string MemberId { get; set; } = default!;

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
   
    [JsonPropertyName("givbux_code")]
    public string? GivBuxCode { get; set; }
}