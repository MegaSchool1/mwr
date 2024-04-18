namespace MegaSchool1.Model.UI;

public record Content(string Message, string Tooltip, string YouTubeId);

public record Shareable(string Description, Content English, Content? Spanish, string ImageUrl, TimeSpan? Duration, string PreviewUrl)
{
    public static string VideoShareable(string heading, string videoUrl, TimeSpan videoLength)
        => $"{heading}{Environment.NewLine}{Constants.PointingDownEmoji}{Environment.NewLine}({Util.MinuteEstimate(videoLength)}min){Environment.NewLine}{videoUrl}";
}

