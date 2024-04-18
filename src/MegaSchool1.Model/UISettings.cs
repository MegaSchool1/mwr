using System.Text.Json.Serialization;

namespace MegaSchool1.Model;

public class UISettings
{
    [JsonPropertyName("localizations")]
    public I18N[] Localizations { get; set; } = default!;

    public I18N EnglishLocale => Localizations.First(l => l.Language == "en-US");
    public I18N SpanishLocale => Localizations.First(l => l.Language == "es-MX");
}