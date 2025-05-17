using Foundation.Model;
using MegaSchool1.Model;
using MegaSchool1.Model.Dto;
using MegaSchool1.ViewModel;
using OneOf;
using OneOf.Types;
using Riok.Mapperly.Abstractions;

namespace MegaSchool1;

[Mapper]
public partial class Mappers
{
    [MapProperty(nameof(EventDto.StartDate), nameof(EventViewModel.StartDate), Use = nameof(NullableToOption))]
    public partial EventViewModel EventDtoToEventViewModel(EventDto eventDto);

    public static OneOf<Video, None> TestimonialToVideo(Testimonial testimonial) => GetVideo(testimonial.Video);

    private static OneOf<Video, None> GetVideo(ShareableDto dto)
    {
        OneOf<Video, None> video = dto.Platform switch
        {
            VideoPlatform.YouTube => !string.IsNullOrWhiteSpace(dto.Id) ? (Video)new YouTube(dto.Id, !string.IsNullOrWhiteSpace(dto.ClipId) && !string.IsNullOrWhiteSpace(dto.ClipTimestamp) ? (dto.ClipId, dto.ClipTimestamp) : new None()) : new None(),
            VideoPlatform.TikTok => !string.IsNullOrWhiteSpace(dto.Id) && !string.IsNullOrWhiteSpace(dto.UserHandle) ? (Video)new TikTok(dto.UserHandle, dto.Id) : new None(),
            VideoPlatform.Vimeo => !string.IsNullOrWhiteSpace(dto.Id) ? (Video)new Vimeo(dto.Id, !string.IsNullOrWhiteSpace(dto.Hash) ? dto.Hash : new None()) : new None(),
            VideoPlatform.Facebook => !string.IsNullOrWhiteSpace(dto.Id) && !string.IsNullOrWhiteSpace(dto.UserHandle) ? (Video)new Facebook(dto.UserHandle, dto.Id) : new None(),
            VideoPlatform.StartMeeting => !string.IsNullOrWhiteSpace(dto.Id) ? (Video)new StartMeeting(dto.Id) : new None(),
            VideoPlatform.Html5 => Uri.IsWellFormedUriString(dto.Url, UriKind.Absolute) ? (Video)new Html5(new(dto.Url)) : new None(),
            VideoPlatform.Wistia => !string.IsNullOrWhiteSpace(dto.Id) ? (Video)new Wistia(dto.Id) : new None(),
            VideoPlatform.Rumble => !string.IsNullOrWhiteSpace(dto.Id) ? (Video)new Rumble(dto.Id) : new None(),
            _ => new None()
        };

        video = video.MapT0(v =>
        {
            v.Start = dto.Start.ToOption();

            return v;
        });
        
        return video;
    }

    public ShareableViewModel ShareableDtoToViewModel(ShareableDto dto)
    {
        var viewModel = new ShareableViewModel();

        viewModel.Id = dto.ContentId;
        viewModel.Video = GetVideo(dto).MapT0(v => new VideoViewModel(v, dto.Duration ?? TimeSpan.MinValue));
        viewModel.Url = !string.IsNullOrWhiteSpace(dto.Url) ? dto.Url : new None();
        viewModel.AppDescription = dto.AppTitle;
        viewModel.Title = dto.ShareableTitle;
        viewModel.ShareableImage = dto.Image ?? (!string.IsNullOrWhiteSpace(dto.ImageUrl) ? (OneOf<Image, Uri, None>)new Uri(dto.ImageUrl) : new None());
        viewModel.CapturePageImage = dto.CapturePageImage ?? (!string.IsNullOrWhiteSpace(dto.CapturePageImageUrl) ? (OneOf<Image, Uri, None>)new Uri(dto.CapturePageImageUrl) : new None());
        viewModel.Download = !string.IsNullOrWhiteSpace(dto.DownloadUrl) ? (!string.IsNullOrWhiteSpace(dto.DownloadText) ? dto.DownloadText : dto.DownloadUrl, new Uri(dto.DownloadUrl)) : new None();

        var validPromo =
            // valid promo prompt
            !string.IsNullOrWhiteSpace(dto.Promo)
            &&
            // valid promo expiration
            (dto.PromoExpiration != null && dto.PromoExpiration > DateTimeOffset.Now);
        viewModel.Promo = validPromo ? (dto.Promo!, dto.PromoExpiration!.Value) : new None();

        viewModel.Event = dto.Event != null ? EventDtoToEventViewModel(dto.Event) : new None();
        viewModel.ShowBusinessSignUp = dto.ShowBusinessSignUp;
        viewModel.HideShortCodePrompt = dto.HideShortCodePrompt;
        viewModel.Images = dto.Images.Any() ? dto.Images : new None();
        viewModel.AuxiliaryText = !string.IsNullOrWhiteSpace(dto.AuxiliaryText) ? dto.AuxiliaryText : new None();
        viewModel.AuxiliaryTexts = dto.AuxiliaryTexts.Any() ? dto.AuxiliaryTexts : new None();
        viewModel.Metadata = !string.IsNullOrWhiteSpace(dto.Metadata) ? dto.Metadata : new None();
        viewModel.FlyerImage = Uri.IsWellFormedUriString(dto.FlyerImageUrl, UriKind.RelativeOrAbsolute) ? new Uri(dto.FlyerImageUrl) : new None();
        viewModel.ShowHeaderImage = dto.ShowHeaderImage;

        return viewModel;
    }
    
    public static OneOf<DateTimeOffset, None> NullableToOption(DateTimeOffset? value) => value.ToOption();
}