using System.Text.Json.Serialization;
using MegaSchool1.Model;

namespace MegaSchool1.Repository.Model;

public class GlobalData
{
    [JsonPropertyName("user_data")]
    public UserData? UserData { get; set; }

    [JsonPropertyName("settings")]
    public Settings? Settings { get; set; }
}