using MegaSchool1.Model.API;
using System.Text.Json.Serialization;

namespace MegaSchool1.Model.Repository;

public record TeamMember
{
    public DisplayName Name => (DisplayName)QMD ?? MemberId;

    public QMD? QMD { get; set; }

    public string CustomName { get; set; } = default!;

    [JsonPropertyName("member_id")]
    public string MemberId { get; set; } = default!;

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }
   
    [JsonPropertyName("givbux_code")]
    public string? GivBuxCode { get; set; }
}