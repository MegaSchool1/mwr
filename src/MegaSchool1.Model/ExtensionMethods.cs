using Foundation.Model;
using MegaSchool1.Model.Dto;
using OneOf.Types;

namespace MegaSchool1.Model;

public static class ExtensionMethods
{ 
    public static int CeilingInt(this double value) => (int)Math.Round(value, MidpointRounding.ToEven);

    public static ShareableDto? Content(this ShareableDto[] videos, Content content)
        => videos.FirstOrDefault(v => v.ContentId == content);

    public static string? MinimalistUrl(this ShareableDto video)
        => video.Platform switch
        {
            VideoPlatform.YouTube => Constants.MinimalistYouTubeLink(video.Id!, new None(), video.Start.ToOption()),
            VideoPlatform.Vimeo => $"{Constants.MinimalistVimeoLink(video.Id!, video.Hash.ToOption(), video.Start.ToOption())}",
            VideoPlatform.TikTok => Constants.MinimalistTikTokLink(video.UserHandle!, video.Id),
            _ => null
        };
}
