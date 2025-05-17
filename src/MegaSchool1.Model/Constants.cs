using System.Globalization;
using MegaSchool1.Model.UI;
using Microsoft.AspNetCore.Components;
using OneOf.Types;
using OneOf;
using System.Web;
using Foundation.Model;
using Microsoft.AspNetCore.Http;

namespace MegaSchool1.Model;

public enum Highlight
{
    None,
    MoneyChallenge,
    ReduceMyTaxes,
    EliminateMyDebt,
    LowerMyBills,
    RestoreMyCredit,
    DCA,
    PRA,
    LandBanking,
    PreciousMetals,
    TrustMyAssets,
    KeysToHomeOwnership,
    Bonuses,
    Residuals,
    Couple,
    Military,
    GenYGenZ,
}

public enum Image
{
    None = 0,
    MoneyChallengeLogo = 1,
    MWRBanner = 2,
    HealthShare = 3,
    MembershipLogo = 4,
    AppScreenshot = 5,
    Overview1On1English = 6,
    RevenueShare1On1English = 7,
    Overview1On1Spanish = 8,
    RevenueShare1On1Spanish = 9,
    PreciousMetals = 10,
    FaithAndFinance = 11,
    NextLevelStrategies = 12,
    StudentLoanDebtReliefTile = 13,
    KeysToHomeOwnership = 14,
    WealthWorksheet = 15,
    MoneyChallengeTransparent = 16,
    MWRLogoTransparent = 17,
    GivBux = 18,
    AppLogo = 19,
    MWRGivBuxLogo = 20,
    Bitcoin = 21,
    Fundable360Logo = 22,
    Fundable360LogoExtended = 23,
}

public enum Strategy
{
    MegaSchool = 0,
    Corporate = 1,
    ExtraDigitMovement = 2,
    FaithAndFinance = 3,
    RealEstate = 4,
    Latino = 5,
}

public enum VideoPlatform
{
    None = 0,
    YouTube = 1,
    Vimeo = 2,
    TikTok = 3,
    Facebook = 4,
    StartMeeting = 5,
    Html5 = 6,
    Wistia = 7,
    Rumble = 8,
}

public enum Content
{
    None = 0,
    MoneyChallenge = 1,
    MoneyChallengeFAQ = 2,
    CorporateBusinessOverview = 3,
    EDMBusinessOverview = 4,
    EDMNeedMoreInfo = 5,
    ReduceMyTaxesExplainer = 6,
    EliminateMyDebtExplainer = 7,
    InvestmentsExplainer = 8,
    TrustsExplainer = 9,
    RevenueShareExplainer = 10,
    Membership = 11,
    CreditRestoration = 12,
    HealthShare = 13,
    KeysToHomeOwnership = 14,
    PreciousMetals = 15,
    TrustMyAssets = 16,
    RealEstatePros = 17,
    ProtectMyAssets = 18,
    StructureMyLegacy = 19,
    FaithAndFinance = 20,
    NextLevelStrategies = 21,
    StudentLoanDebtRelief = 22,
    MichelleEliseFinancialLiteracy = 23,
    MichelleEliseInstantPayRaise = 24,
    MichelleEliseHealthShare = 25,
    TeenCarPurchase = 26,
    MembershipBasedBusiness = 27,
    WealthWorksheet = 28,
    GivBux = 29,
    GivBuxMerchant = 30,
    EDMGivBux = 31,
    MegaSchoolAppInstall = 32,
    GivBuxOpportunity = 33,
    GivBuxCharity = 34,
    EDMPique = 35,
    Opportunity202407 = 36,
    MS1Opportunity202407 = 37,
    GivBuxFundraiser = 38,
    GivBuxAccountSetup = 39,
    GivBuxUberDemo = 40,
    MS1Opportunity = 41,
    FaithAndFinanceOverview = 42,
    CreditTeamOpportunity = 43,
    FastStart = 44,
    HowBanksMakeMoney = 45,
    PayoffMortgage = 46,
    MembershipOrientation = 47,
    MyRewards = 48,
    BusinessOwnerPique = 49,
    HBBTaxBenefits = 50,
    BizOwnerPique = 51,
    BitcoinPreview = 52,
    Custom = 53,
    PRA = 54,
    PrisonMinistry = 55,
    SchoolFundraiser = 56,
    FlowPreview = 57,
    CarPayoff1 = 58,
    CarPayoff2 = 59,
    WhatWealthyDo = 60,
    HappyRetirement = 61,
    MWRPique = 62,
    Promo = 63,
    MWR30SecTeaser = 64,
    MWR60SecTeaser = 65,
    MWR30SecReel = 66,
    MWR60SecTeaser2 = 67,
    MilitaryPique = 68,
    FaithAndFinanceTeaser = 69,
    MLMPique = 70,
    MembershipPique = 71,
    MindsetPique = 72,
    MWR60SecTeaser3 = 73,
    DCAExplainer1 = 74,
    MWR2MinTeaser1 = 75,
    JoinMWR = 76,
    DebtDemo = 77,
    CollegeFund = 78,
    BusinessFunding = 79,
    BusinessFundingPique = 80,
    Fundable360 = 81,
    CollegeFundingPique = 82,
    Fundable360Explainer = 83,
    Fundable360Training = 84,
    Taxed5Percent = 85,
    Fundable360Pique = 86,
    RealEstateProsExt = 87,
    Podcast1 = 88,
    MoneyChallenge2 = 89,
    InstantPayRaise = 90,
    LowerMyBills = 91,
}

public enum Language
{
    None = 0,
    English = 1,
    Spanish = 2,
}

public enum ExtraDigitMovementPipeline
{
    None = 0,
    Enqueue = 1,
    Question = 2,
    Connection = 3,
    Invitation = 4,
    Decision = 5,
}

public enum MegaSchoolPipeline
{
    None = 0,
    Enqueue = 1,
    ListenAndAsk = 2,
    Share = 3,
    Connect = 4,
    Decision = 5,
}

public enum LivestreamPlatform
{
    Facebook = 0,
    YouTube = 1,
    LinkedIn = 2,
}

public enum ProspectVersion
{
    v1_0_0_0 = 0,
}

public record YouTube(string VideoId, OneOf<(string Id, string Timestamp), None> Clip);
public record TikTok(string UserHandle, string VideoId);

public record Facebook(string ChannelId, string VideoId);

public record StartMeeting(string VideoId);

public record Html5(Uri Uri);

public record Wistia(string VideoId);

public record Rumble(string VideoId);

[GenerateOneOf]
public partial class Video : OneOfBase<YouTube, TikTok, Vimeo, Facebook, StartMeeting, Html5, Wistia, Rumble>
{
    public OneOf<TimeSpan, None> Start { get; set; }
}

public class Constants(UISettings ui, NavigationManager navigationManager)
{
    public static readonly Content[] GivBuxContent = [Content.GivBux, Content.GivBuxMerchant, Content.GivBuxCharity, Content.EDMGivBux, Content.GivBuxOpportunity, Content.EDMNeedMoreInfo, Content.WealthWorksheet, Content.MS1Opportunity202407, Content.MS1Opportunity, Content.GivBuxFundraiser, Content.GivBuxAccountSetup, Content.GivBuxUberDemo];
    public static readonly Content[] OrderedContent = GivBuxContent.Union(Enum.GetValues<Content>().Except(GivBuxContent).Except([Content.None, Content.TeenCarPurchase])).ToArray();

    public static readonly TimeZoneInfo NewYorkTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
    public static readonly TimeZoneInfo ChicagoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");
    public static readonly TimeZoneInfo LosAngelesTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Los_Angeles");
    public static readonly TimeZoneInfo DefaultTimeZone = NewYorkTimeZone;

    public static readonly string PointingDownEmoji = $"\ud83d\udc47";
    public static readonly string JeromePointingDownEmoji = "\ud83d\udc47\ud83c\udffd";
    public static readonly string MultiPlatformLivestreamUrlPlaceholder = "{Corporate.Livestream}";
    public static readonly string YouTubeEmbedLinkPrefix = "https://www.youtube.com/embed/";
    public static readonly string VimeoEmbedLinkPrefix = "https://player.vimeo.com/video/";
    public static readonly string MinimalistVideoLinkPrefix = "https://megaschool.me/v";
    public const string AppInstallTutorialUrl = "https://video.wixstatic.com/video/5f35ec_33bda4fc60fd41cf8c3a09924f204746/480p/mp4/file.mp4";

    public static string BusinessEnrollmentUrl(string username) => $"https://user.mwrfinancial.com/{username}/join";
    public static string MembershipEnrollmentUrl(string username) => $"https://user.mwrfinancial.com/{username}/signup-financialedge";
    public static string InstantPayRaiseUrlEnglish(string username) => $"https://www.mwrfinancial.com/iprr/?member={username}";
    public static string InstantPayRaiseUrlSpanish(string username) => $"https://www.mwrfinancial.com/es/iprr/?member={username}";
    public static string MarketingDirectorUrlEnglish(string username) => $"https://www.makewealthreal.com/?member={username}";
    public static string MarketingDirectorUrlSpanish(string username) => $"https://www.makewealthreal.com/es/?member={username}";
    public static string JoinMakeWealthReal(string username, Language language) => $"https://www.makewealthreal.com{(language == Language.Spanish ? "/es" : string.Empty)}/get-started/?member={username}";

    public static string MinimalistYouTubeLink(string youTubeId, OneOf<(string Id, string Timestamp), None> clipInfo, OneOf<TimeSpan, None> videoStart)
    {
        var videoParameters = QueryString.Create("y", youTubeId);
        
        // clip
        if (clipInfo.TryPickT0(out var clip, out _))
        {
            videoParameters = videoParameters.Add("clip", clip.Id).Add("clipt", clip.Timestamp);
        }
        
        // start time
        if (videoStart.TryPickT0(out var start, out _))
        {
            videoParameters = videoParameters.Add("s", ((int)start.TotalSeconds).ToString());
        }
        
        return $"{MinimalistVideoLinkPrefix}{videoParameters.ToString()}";
    }

    public static string MinimalistVimeoLink(string vimeoId, OneOf<string, None> hash, OneOf<TimeSpan, None> start)
    {
        var uriBuilder = new UriBuilder($"{MinimalistVideoLinkPrefix}?v={vimeoId}");
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        
        // hash
        if (hash.TryPickT0(out var h, out _))
        {
            query["h"] = h;
        }
        
        // start time
        if (start.TryPickT0(out var s, out _))
        {
            query["s"] = s.TotalSeconds.ToString();
        }
        
        uriBuilder.Query = query.ToString();
        
        return uriBuilder.ToString().Replace(":443", string.Empty);
    }
        
    public static string MinimalistTikTokLink(string tikTokHandle, string videoId) => $"{MinimalistVideoLinkPrefix}?th={tikTokHandle}&t={videoId}";
    public static string Fundable360CapturePage(string agentId) => $"https://www.makewealthreal.com/fundable360/?member={agentId}";

    public static string MinimalistVideoLink(Video video) => video.Match(
        youTube => MinimalistYouTubeLink(youTube.VideoId, youTube.Clip, video.Start),
        tikTok => MinimalistTikTokLink(tikTok.UserHandle, tikTok.VideoId),
        vimeo => MinimalistVimeoLink(vimeo.VideoId, vimeo.Hash, video.Start),
        facebook => $"https://www.facebook.com/watch/live/?ref=watch_permalink&v={facebook.VideoId}",
        startMeeting => $"https://stme.in/{startMeeting.VideoId}",
        html5 => html5.Uri.AbsoluteUri,
        wistia => $"{MinimalistVideoLinkPrefix}?w={wistia.VideoId}",
        rumble => $"https://rumble.com/embed/{rumble.VideoId}/?pub=4");

    public static string GetImageUriOrDefault(Image image) => GetImageUri(image).Match(found => found, none => GetImageUrl(Image.MWRLogoTransparent));
    
    public static OneOf<string, None> GetImageUri(Image image)
    {
        try
        {
            return GetImageUrl(image);
        }
        catch (Exception)
        {
            return new None();
        }
    }

    public static string GetImageUrl(Image image) => image switch
    {
        Image.MWRBanner => "/images/mwr-banner.png",
        Image.HealthShare => "/images/mwr-healthshare.png",
        Image.MembershipLogo => "/images/mwr-membership-logo.jpg?v=2",
        Image.MoneyChallengeLogo => "/images/72hour-money-challenge-logo.png",
        Image.AppScreenshot => "/images/app-screenshot.jpeg",
        Image.Overview1On1English => "/images/72-HourMoneyChallengeOverview_1on1_ENG.png",
        Image.RevenueShare1On1English => "/images/72-HourMoneyChallengeRevenueSharing-1on1-ENG.png",
        Image.Overview1On1Spanish => "/images/72-HourMoneyChallengeOverview_1on1_SPANISH.png",
        Image.RevenueShare1On1Spanish => "/images/72-HourMoneyChallengeRevenueSharing-1on1-SPANISH.png",
        Image.PreciousMetals => "/images/mwr-precious-metals.jpg",
        Image.FaithAndFinance => "/images/faithandfinance.jpg",
        Image.NextLevelStrategies => "/images/next-level-strategies-logo.png",
        Image.StudentLoanDebtReliefTile => "/images/student-loan-debt-relief-tile.png",
        Image.KeysToHomeOwnership => "/images/keys-to-home-ownership-banner.jpg",
        Image.WealthWorksheet => "https://static.wixstatic.com/media/5f35ec_f9531ebb4a2d451cac0d6dd5b588c474~mv2.png",
        Image.MoneyChallengeTransparent => "/images/72-hour-money-challenge.png",
        Image.MWRLogoTransparent => "/images/mwr-logo-transparent-221x221.png",
        Image.GivBux => "/images/givbux.jpeg",
        Image.AppLogo => "/images/app-logo.png",
        Image.MWRGivBuxLogo => "/images/mwr-givbux-logo.png",
        Image.Bitcoin => "/images/bitcoin.png",
        Image.Fundable360Logo => "https://static.wixstatic.com/media/5f35ec_a147b37684694e2caa99fa301539a6ad~mv2.jpeg",
        Image.Fundable360LogoExtended => "https://static.wixstatic.com/media/5f35ec_7b5683f431f14509a63432b25ef8c1a1~mv2.jpeg",
        _ => throw new Exception($"Image not found: {image}"),
    };

    public static string? GetBusinessEnrollmentPromo(string memberId) => null;

    public static string GetCapturePage(Content content, Language language, NavigationManager navigationManager, string memberId, string referralId)
        => $"{navigationManager.BaseUri}{(language == Language.Spanish ? "es" : "en")}/{content}/{memberId}/{HttpUtility.UrlEncode(referralId)}/0";
    
    public static readonly Dictionary<LivestreamPlatform, string> CorporateLivestreamLink = new()
    {
        { LivestreamPlatform.Facebook, "https://www.mwr.live/" },
        { LivestreamPlatform.YouTube, "https://www.youtube.com/@MWRFinancial/streams" },
        { LivestreamPlatform.LinkedIn, "https://www.linkedin.com/company/mwr-financial-official/mycompany/" },
    };

    public static readonly Dictionary<Rank, (int NumMemberships, int MonthlyPay, string Title)> DailyGuarantee = new()
    {
        { Rank.None, (0, 0, "N/A") },
        { Rank.ExecutiveDirector1, (3, 150, "Executive Director") },
        { Rank.ExecutiveDirector2, (12, 600, "2* Executive Director") },
        { Rank.ExecutiveDirector3, (50, 1500, "3* Executive Director") },
        { Rank.ExecutiveDirector4, (90, 3000, "4* Executive Director") },
        { Rank.ExecutiveDirector5, (180, 4500, "5* Executive Director") },
        { Rank.Regional1, (300, 6000, "Regional Director") },
        { Rank.Regional2, (480, 9000, "2* Regional Director") },
        { Rank.Regional3, (750, 12000, "3* Regional Director") },
        { Rank.Regional4, (1200, 19500, "4* Regional Director") },
        { Rank.Regional5, (1800, 30000, "5* Regional Director") },
        { Rank.NationalDirector, (3000, 45000, "National Director") },
        { Rank.VicePresidentialDirector, (6000, 90000, "Vice Presidential Director") },
        { Rank.PresidentialDirector, (12000, 150000, "Presidential Director") },
        { Rank.ExecutiveChairman, (21000, 300000, "Executive Chairman") },
        { Rank.NationalAmbassador, (33000, 450000, "National Ambassador") },
    };

    private readonly Dictionary<Strategy, Shareable> _moneyChallengeShareable = new()
    {
        {
            Strategy.Corporate,
            new ($"72-Hour Money Challenge",
                    new(Shareable.VideoShareable("72-Hour Money Challenge!", ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl()!, new(0, 0, 1, 30)), "English shareable copied!", ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl()!),
                    new(Shareable.VideoShareable("72-Hour Money Challenge!", ui.SpanishLocale[Content.MoneyChallenge]!.MinimalistUrl()!, new(0, 0, 1, 53)), "Español shareable copied!", ui.SpanishLocale[Content.MoneyChallenge]!.MinimalistUrl()!),
                    GetImageUrl(Image.MoneyChallengeLogo),
                    new(0, 1, 30),
                    ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl()!)
        },
        {
            Strategy.ExtraDigitMovement,
            new ($"72-Hour Money Challenge Overview",
                    new(Shareable.VideoShareable("72-Hour Money Challenge!", ui.EnglishLocale[Content.EDMBusinessOverview]!.MinimalistUrl()!, new(0, 0, 15, 0)), "Shareable copied!", ui.EnglishLocale[Content.EDMBusinessOverview]!.MinimalistUrl()!),
                    null,
                    GetImageUrl(Image.MoneyChallengeLogo),
                    new(0, 15, 0),
                    ui.EnglishLocale[Content.EDMBusinessOverview]!.MinimalistUrl()!)
        },
        {
            Strategy.MegaSchool,
            new ($"72-Hour Money Challenge",
                    new($"{Shareable.VideoShareable("72-Hour Money Challenge!", ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl()!, new(0, 0, 1, 30))}{Environment.NewLine}{Environment.NewLine}{Shareable.VideoShareable("FAQ", ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl()!, new(0, 0, 5, 0))}", "Shareable copied!",  ui.EnglishLocale[Content.MoneyChallenge]!.Id),
                    new($"{Shareable.VideoShareable("72-Hour Money Challenge!", ui.SpanishLocale[Content.MoneyChallenge]!.MinimalistUrl()!, new(0, 0, 1, 30))}{Environment.NewLine}{Environment.NewLine}{Shareable.VideoShareable("FAQ", ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl()!, new(0, 0, 5, 0))}", "Shareable copied!", ui.SpanishLocale[Content.MoneyChallenge]!.Id),
                    GetImageUrl(Image.MoneyChallengeLogo),
                    new(0, 7, 0),
                    ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl()!)
        },
    };
    public Dictionary<Strategy, Shareable> MoneyChallenge => _moneyChallengeShareable;

    public static readonly Shareable AppShareable = new(
        $"App Download",
        new($"Mega School App{Environment.NewLine}{PointingDownEmoji}{Environment.NewLine}https://megaschool1.github.io/", "Shareable copied!", "https://www.ms1.megaschool.me/72hr-money-challenge"),
        null,
        GetImageUrl(Image.AppScreenshot),
        null,
        null!);

    private readonly Shareable _moneyChallengeFAQ = new(
        "72-Hour Money Challenge FAQ",
        new(Shareable.VideoShareable("72-Hour Money Challenge - FAQ", ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl()!, new(0, 0, 5, 0)), "Shareable copied!", ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl()!),
        null,
        GetImageUrl(Image.MoneyChallengeLogo),
        new(0, 0, 5, 0),
        ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl()!);
    public Shareable MoneyChallengeFAQ => _moneyChallengeFAQ;
}
