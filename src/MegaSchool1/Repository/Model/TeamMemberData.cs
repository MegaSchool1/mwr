using System.Security.Cryptography;
using System.Text.Json.Serialization;
using MegaSchool1.Model;

namespace MegaSchool1.Repository.Model;

public class TeamMemberData
{
    [JsonPropertyName("member")]
    public required TeamMember Member { get; set; } = default!;

    [JsonPropertyName("data")]
    public UserData Data { get; set; } = new();
}