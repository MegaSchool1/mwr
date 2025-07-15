using OneOf;
using OneOf.Types;
using ValueOf;

namespace Foundation.Model;

//public class Percentage : ValueOf<decimal, Percentage>;

/// <summary>
///     A percentage formated as a percentage, ie 12% rather than 0.12.
/// </summary>
public class PercentageFormat
{
    private PercentageFormat() { }

    public static PercentageFormat Instance = new();
}

/// <summary>
///     A percentage formated as a decmial, ie 0.12 rather than 12%.
/// </summary>
public class DecimalFormat
{
    private DecimalFormat() { }

    public static DecimalFormat Instance = new();
}

public sealed class Percentage
{
    private readonly decimal _value;
    private readonly OneOf<DecimalFormat, PercentageFormat> _format;

    public decimal ToFormat(OneOf<DecimalFormat, PercentageFormat> format)
        => _format.Match(
            decimalFormat => format.Match(d => _value, p => _value / 100),
            percentageFormat => format.Match(d => _value / 100, p => _value));
    
    public Percentage(decimal value, OneOf<DecimalFormat, PercentageFormat> format)
    {
        _value = value;
        _format = format;
    }
}

public class Scalar : ValueOf<int, Scalar>;

/// <summary>
///     Hash - Required for private Vimeo videos. See https://www.drupal.org/project/video_embed_field/issues/3238136
/// </summary>
public record Vimeo(string VideoId, OneOf<string, None> Hash);