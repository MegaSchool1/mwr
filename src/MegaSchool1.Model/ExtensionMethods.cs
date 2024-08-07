namespace MegaSchool1.Model;

public static class ExtensionMethods
{
    public static VideoResource? Content(this VideoResource[] videos, Content content)
        => videos.FirstOrDefault(v => v.ContentId == content);

    public static string? MinimalistUrl(this VideoResource video)
        => video.Platform switch
        {
            VideoPlatform.YouTube => Constants.MinimalistYouTubeLink(video.Id),
            VideoPlatform.Vimeo => $"{Constants.MinimalistVimeoLink(video.Id, video.Hash)}",
            VideoPlatform.TikTok => Constants.MinimalistTikTokLink(video.UserHandle!, video.Id),
            _ => null
        };

    public static string? ShareableUrl(this VideoResource video) => video.MinimalistUrl() ?? video.Url;

    public static string EmbeddableUrl(this VideoResource video)
        => video.Platform switch
        {
            VideoPlatform.YouTube => Constants.EmbeddableYouTubeLink(video.Id),
            VideoPlatform.Vimeo => $"{Constants.EmbeddableVimeoLink(video.Id)}{(string.IsNullOrWhiteSpace(video.Hash) ? string.Empty : $"&h={video.Hash}")}",
            _ => throw new Exception("No video set!")
        };
}
