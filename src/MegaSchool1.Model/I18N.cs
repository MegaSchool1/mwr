using System.Text.Json.Serialization;

namespace MegaSchool1.Model;

public class I18N
{
    [JsonPropertyName("language")]
    public string Language { get; set; } = default!;

    [JsonPropertyName("videos")]
    public VideoResource[] Videos { get; set; } = [];

    [JsonPropertyName("testimonials")]
    public Testimonial[] Testimonials { get; set; } = [];

    [JsonPropertyName("events")]
    public EventDto[] Events { get; set; } = [];

    public VideoResource? this[Content content]
    {
        get
        {
            return Videos.Content(content);
        }
    }
}