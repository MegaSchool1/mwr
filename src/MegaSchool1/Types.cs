using OneOf;
using OneOf.Types;

public record Pixel(int Value);
public record Percentage(double Value);
public record YouTube(string VideoId);
public record TikTok(string UserHandle, string VideoId);

/// <summary>
///     Hash - Required for private Vimeo videos. See https://www.drupal.org/project/video_embed_field/issues/3238136
/// </summary>
public record Vimeo(string VideoId, OneOf<string, None> Hash);

public record Facebook (string ChannelId, string VideoId);