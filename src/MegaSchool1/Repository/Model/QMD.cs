using System.Text.Json.Serialization;
using MegaSchool1.Model;

namespace MegaSchool1.Repository.Model;

public class QMD
{
    [JsonPropertyName("Message")]
    public string? Message { get; set; }

    [JsonPropertyName("ProfilePicUrl")]
    public string? ProfilePictureUrl { get; set; }

    [JsonPropertyName("ProfilePicName")]
    public string? ProfilePictureName { get; set; }

    [JsonPropertyName("FirstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("LastName")]
    public string? LastName { get; set; }

    [JsonPropertyName("Email")]
    public string? Email { get; set; }

    [JsonPropertyName("PhoneNo")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("CurrentPosition")]
    public string? CurrentPosition { get; set; }

    [JsonPropertyName("BusnmShow")]
    public bool BusnmShow { get; set; }

    [JsonPropertyName("BusinessName")]
    public string? BusinessName { get; set; }
}