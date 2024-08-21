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
    public partial EventViewModel EventDtoToEventViewModel(EventDto eventDto);

    public ShareableViewModel ShareableDtoToViewModel(ShareableDto dto)
    {
        var viewModel = new ShareableViewModel();

        viewModel.Id = dto.ContentId;

        OneOf<YouTube, TikTok, Vimeo, Facebook, None> video = dto.Platform switch
        {
            VideoPlatform.YouTube => !string.IsNullOrWhiteSpace(dto.Id) ? new YouTube(dto.Id) : new None(),
            VideoPlatform.TikTok => !string.IsNullOrWhiteSpace(dto.Id) && !string.IsNullOrWhiteSpace(dto.UserHandle) ? new TikTok(dto.UserHandle, dto.Id) : new None(),
            VideoPlatform.Vimeo => !string.IsNullOrWhiteSpace(dto.Id) ? new Vimeo(dto.Id, !string.IsNullOrWhiteSpace(dto.Hash) ? dto.Hash : new None()) : new None(),
            VideoPlatform.Facebook => !string.IsNullOrWhiteSpace(dto.Id) && !string.IsNullOrWhiteSpace(dto.UserHandle) ? new Facebook(dto.UserHandle, dto.Id) : new None(),
            _ => new None()
        };

        var duration = dto.Duration ?? TimeSpan.MinValue;

        viewModel.Video = video.Match(
            youTube => (OneOf<VideoViewModel, None>) new VideoViewModel(youTube, duration),
            tikTok => new VideoViewModel(tikTok, duration),
            vimeo => new VideoViewModel(vimeo, duration),
            facebook => new VideoViewModel(facebook, duration),
            none => none);

        viewModel.Url = !string.IsNullOrWhiteSpace(dto.Url) ? dto.Url : new None();
        viewModel.AppDescription = dto.AppTitle;
        viewModel.Title = dto.ShareableTitle;
        viewModel.ShareableImage = dto.Image ?? viewModel.ShareableImage;
        viewModel.CapturePageImage = dto.CapturePageImage ?? viewModel.CapturePageImage;
        viewModel.Download = !string.IsNullOrWhiteSpace(dto.DownloadUrl) ? (!string.IsNullOrWhiteSpace(dto.DownloadText) ? dto.DownloadText : dto.DownloadUrl, new Uri(dto.DownloadUrl)) : new None();

        var validPromo =
            // valid promo prompt
            !string.IsNullOrWhiteSpace(dto.Promo)
            &&
            // valid promo expiration
            dto.PromoExpiration != null;
        viewModel.Promo = validPromo ? (dto.Promo!, dto.PromoExpiration!.Value) : ("For $100 off, text", Constants.FinancialIndependenceMonthPromoExpiration);

        viewModel.Event = dto.Event != null ? EventDtoToEventViewModel(dto.Event) : new None();
        viewModel.ShowBusinessSignUp = dto.ShowBusinessSignUp;
        viewModel.HideShortCodePrompt = dto.HideShortCodePrompt;
        viewModel.Images = dto.Images.Any() ? dto.Images : new None();
        viewModel.AuxiliaryText = !string.IsNullOrWhiteSpace(dto.AuxiliaryText) ? dto.AuxiliaryText : new None();
        viewModel.Metadata = !string.IsNullOrWhiteSpace(dto.Metadata) ? dto.Metadata : new None();

        return viewModel;
    }
}