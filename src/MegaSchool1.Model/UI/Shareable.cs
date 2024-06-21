namespace MegaSchool1.Model.UI;

public record Content(string Message, string Tooltip, string YouTubeId);

public record Shareable(string Description, Content English, Content? Spanish, string ImageUrl, TimeSpan? Duration, string PreviewUrl)
{
    public static string VideoShareable(VideoResource video) => VideoShareable(video.ShareableTitle, video.Platform == VideoPlatform.None ? video.Url : video.MinimalistUrl(), video.Duration);

    public static string VideoShareable(string heading, string videoUrl, TimeSpan? videoLength)
    {
        var durationEstimate = videoLength == null ? 0 : Util.MinuteEstimate(videoLength.Value);

        return $"{heading}{Environment.NewLine}{Constants.PointingDownEmoji}{Environment.NewLine}{(durationEstimate == 0 ? string.Empty : $"({(videoLength?.Hours > 0 ? $"{videoLength?.Hours}hr " : string.Empty)}{durationEstimate}min){Environment.NewLine}")}{videoUrl}";
    }
}

