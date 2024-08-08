using System.Text.Json.Serialization;
using MegaSchool1.Model.Dto;

namespace MegaSchool1.Model;

public class I18N
{
    [JsonPropertyName("language")]
    public string Language { get; set; } = default!;

    [JsonPropertyName("videos")]
    public ShareableDto[] Videos { get; set; } = [];

    [JsonPropertyName("testimonials")]
    public Testimonial[] Testimonials { get; set; } = [];

    [JsonPropertyName("events")]
    public EventDto[] Events { get; set; } = [];

    public ShareableDto? this[Content content]
    {
        get
        {
            return Videos.Content(content);
        }
    }
}