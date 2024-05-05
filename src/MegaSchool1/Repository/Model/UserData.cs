using System.Text.Json.Serialization;
using MegaSchool1.Model;

namespace MegaSchool1.Repository.Model;

public class UserData
{
    [JsonPropertyName("prospects")]
    public List<Prospect> Prospects { get; set; } = [];
}