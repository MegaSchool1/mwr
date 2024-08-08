using System.Text.Json.Serialization;
using MegaSchool1.Model.Dto;

namespace MegaSchool1.Model;

public class Testimonial
{
    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("video")]
    public ShareableDto Video { get; set; } = default!;

    [JsonPropertyName("highlights")]
    public Highlight[] Highlights { get; set; } = [];
}
