using OneOf;
using OneOf.Types;
using ValueOf;

namespace Foundation.Model;

public class Percentage : ValueOf<decimal, Percentage>;
public class Scalar : ValueOf<int, Scalar>;

/// <summary>
///     Hash - Required for private Vimeo videos. See https://www.drupal.org/project/video_embed_field/issues/3238136
/// </summary>
public record Vimeo(string VideoId, OneOf<string, None> Hash);