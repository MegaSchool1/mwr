using System.Text.Json.Serialization;

namespace MegaSchool1.Model;

public record EventDto
{
    [JsonPropertyName("header")]
    public string Header { get; set; } = default!;

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("meetingId")]
    public string? MeetingId { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; } = default!;

    [JsonPropertyName("day")]
    public DayOfWeek? Day { get; set; }

    [JsonPropertyName("time")]
    public TimeOnly Time { get; set; }

    [JsonPropertyName("endDate")]
    public DateTimeOffset? EndDate { get; set; }

    [JsonPropertyName("shareableHeader")]
    public string ShareableHeader { get; set; } = default!;

    [JsonPropertyName("shareableHost")]
    public string? ShareableHost { get; set; }

    [JsonPropertyName("shareableGuest")]
    public string? ShareableGuest { get; set; }

    [JsonPropertyName("vanityLink")]
    public string? VanityLink { get; set; }

    [JsonPropertyName("hashTags")]
    public string HashTags { get; set; } = default!;

    [JsonPropertyName("anchor")]
    public string Anchor { get; set; } = default!;

    [JsonPropertyName("archive")]
    public string? Archive { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}
