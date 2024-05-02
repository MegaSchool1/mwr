using MegaSchool1.Model.UI;
using Microsoft.AspNetCore.Components;
using System.Net.NetworkInformation;

namespace MegaSchool1.Model;

public enum Rank
{
    None = 0,
    ExecutiveDirector1 = 1,
    ExecutiveDirector2 = 2,
    ExecutiveDirector3 = 3,
    ExecutiveDirector4 = 4,
    ExecutiveDirector5 = 5,
    Regional1 = 6,
    Regional2 = 7,
    Regional3 = 8,
    Regional4 = 9,
    Regional5 = 10,
    NationalDirector = 11,
    VicePresidentialDirector = 12,
    PresidentialDirector = 13,
    ExecutiveChairman = 14,
    NationalAmbassador = 15,
}

public enum Highlight
{
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
}

public enum Video
{
    None = 0,
    MoneyChallenge = 1,
    MoneyChallengeFAQ = 2,
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
    Share = 2,
    MembershipInvitation = 3,
    MembershipDecision = 4,
    Connection = 5,
    BusinessOpportunityInvitation = 6,
    BusinessOpportunityDecision = 7,
}

public enum LivestreamPlatform
{
    Facebook = 0,
    YouTube = 1,
    LinkedIn = 2,
}

public class Constants(UISettings ui, NavigationManager navigationManager)
{
    public static readonly TimeZoneInfo NewYorkTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
    public static readonly TimeZoneInfo ChicagoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");
    public static readonly TimeZoneInfo LosAngelesTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Los_Angeles");
    public static readonly TimeZoneInfo DefaultTimeZone = NewYorkTimeZone;

    public static readonly string PointingDownEmoji = $"\ud83d\udc47";
    public static readonly string MultiPlatformLivestreamUrlPlaceholder = "{Corporate.Livestream}";
    public static readonly string YouTubeEmbedLinkPrefix = "https://www.youtube.com/embed/";
    public static readonly string VimeoEmbedLinkPrefix = "https://player.vimeo.com/video/";
    public static readonly string MinimalistVideoLinkPrefix = "https://megaschool.me/v";
    public static readonly string MinimalistYouTubeVideoLinkPrefix = $"{MinimalistVideoLinkPrefix}?y=";
    public const string AppInstallTutorialUrl = "https://video.wixstatic.com/video/5f35ec_33bda4fc60fd41cf8c3a09924f204746/480p/mp4/file.mp4";

    private static readonly DateTimeOffset PayItForwardPromoExpiration = new(2024, 5, 7, 11, 59, 59, NewYorkTimeZone.BaseUtcOffset);

    public static string BusinessEnrollmentUrl(string username) => $"https://user.mwrfinancial.com/{username}/join";
    public static string MembershipEnrollmentUrl(string username) => $"https://user.mwrfinancial.com/{username}/signup-financialedge";
    public static string InstantPayRaiseUrlEnglish(string username) => $"https://www.mwrfinancial.com/iprr/?member={username}";
    public static string InstantPayRaiseUrlSpanish(string username) => $"https://www.mwrfinancial.com/es/iprr/?member={username}";
    public static string MarketingDirectorUrlEnglish(string username) => $"https://www.mwrfinancial.com/?member={username}";
    public static string MarketingDirectorUrlSpanish(string username) => $"https://www.mwrfinancial.com/es/?member={username}";
    public static string JoinMakeWealthRealEnglish(string username) => $"https://www.mwrfinancial.com/join/?member={username}";
    public static string JoinMakeWealthRealSpanish(string username) => $"https://www.mwrfinancial.com/es/join/?member={username}";

    public static string MinimalistYouTubeLink(string youTubeId) => $"{MinimalistYouTubeVideoLinkPrefix}{youTubeId}";
    public static string MinimalistVimeoLink(string vimeoId, string? hash) => $"{MinimalistVideoLinkPrefix}?v={vimeoId}{(string.IsNullOrWhiteSpace(hash) ? string.Empty : $"&h={hash}")}";
    public static string MinimalistTikTokLink(string tikTokHandle, string videoId) => $"{MinimalistVideoLinkPrefix}?th={tikTokHandle}&t={videoId}";
    public static string EmbeddableYouTubeLink(string youTubeId) => $"{YouTubeEmbedLinkPrefix}{youTubeId}";
    public static string EmbeddableVimeoLink(string vimeoId) => $"{VimeoEmbedLinkPrefix}{vimeoId}";

    public static string GetImageUrl(Image image) => image switch
    {
        Image.MWRBanner => "/images/mwr-banner.png",
        Image.HealthShare => "/images/mwr-healthshare.png",
        Image.MembershipLogo => "/images/mwr-membership-logo.jpg",
        Image.MoneyChallengeLogo => "/images/72hour-money-challenge-logo.png",
        Image.AppScreenshot => "images/app-screenshot.jpeg",
        Image.Overview1On1English => "images/72-HourMoneyChallengeOverview_1on1_ENG.png",
        Image.RevenueShare1On1English => "images/72-HourMoneyChallengeRevenueSharing-1on1-ENG.png",
        Image.Overview1On1Spanish => "images/72-HourMoneyChallengeOverview_1on1_SPANISH.png",
        Image.RevenueShare1On1Spanish => "images/72-HourMoneyChallengeRevenueSharing-1on1-SPANISH.png",
        Image.PreciousMetals => "images/mwr-precious-metals.jpg",
        Image.FaithAndFinance => "images/faithandfinance.jpg",
        Image.NextLevelStrategies => "images/next-level-strategies-logo.png",
        Image.StudentLoanDebtReliefTile => "/images/student-loan-debt-relief-tile.png",
        _ => throw new Exception($"Image not found: {image}"),
    };

    public static string? GetBusinessEnrollmentPromo(string memberId)
    {
        string? promo;

        if (DateTimeOffset.Now <= PayItForwardPromoExpiration)
        {
            promo = $"{Environment.NewLine}{Environment.NewLine}For $100 off, use coupon code:{Environment.NewLine}{Environment.NewLine}{memberId}code";
        }
        else
        {
            promo = null;
        }

        return promo;
    }

    public string GetCapturePage(Content content, Language language, string memberId, string referralId) => content switch
    {
        Content.MoneyChallenge => $"{navigationManager.BaseUri}{(language == Language.Spanish ? "es/" : string.Empty)}72/{memberId}/{referralId}",
        _ => $"{navigationManager.BaseUri}{(language == Language.Spanish ? "es" : "en")}/{content.ToString().ToLower()}/{memberId}/{referralId}"
    };

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
                    new(Shareable.VideoShareable("72-Hour Money Challenge!", ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl(), new(0, 0, 1, 30)), "English shareable copied!", ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl()),
                    new(Shareable.VideoShareable("72-Hour Money Challenge!", ui.SpanishLocale[Content.MoneyChallenge]!.MinimalistUrl(), new(0, 0, 1, 53)), "Español shareable copied!", ui.SpanishLocale[Content.MoneyChallenge]!.MinimalistUrl()),
                    GetImageUrl(Image.MoneyChallengeLogo),
                    new(0, 1, 30),
                    ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl())
        },
        {
            Strategy.ExtraDigitMovement,
            new ($"72-Hour Money Challenge Overview",
                    new(Shareable.VideoShareable("72-Hour Money Challenge!", ui.EnglishLocale[Content.EDMBusinessOverview]!.MinimalistUrl(), new(0, 0, 15, 0)), "Shareable copied!", ui.EnglishLocale[Content.EDMBusinessOverview]!.MinimalistUrl()),
                    null,
                    GetImageUrl(Image.MoneyChallengeLogo),
                    new(0, 15, 0),
                    ui.EnglishLocale[Content.EDMBusinessOverview]!.MinimalistUrl())
        },
        {
            Strategy.MegaSchool,
            new ($"72-Hour Money Challenge",
                    new($"{Shareable.VideoShareable("72-Hour Money Challenge!", ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl(), new(0, 0, 1, 30))}{Environment.NewLine}{Environment.NewLine}{Shareable.VideoShareable("FAQ", ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl(), new(0, 0, 5, 0))}", "Shareable copied!",  ui.EnglishLocale[Content.MoneyChallenge]!.Id),
                    new($"{Shareable.VideoShareable("72-Hour Money Challenge!", ui.SpanishLocale[Content.MoneyChallenge]!.MinimalistUrl(), new(0, 0, 1, 30))}{Environment.NewLine}{Environment.NewLine}{Shareable.VideoShareable("FAQ", ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl(), new(0, 0, 5, 0))}", "Shareable copied!", ui.SpanishLocale[Content.MoneyChallenge]!.Id),
                    GetImageUrl(Image.MoneyChallengeLogo),
                    new(0, 7, 0),
                    ui.EnglishLocale[Content.MoneyChallenge]!.MinimalistUrl())
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
        new(Shareable.VideoShareable("72-Hour Money Challenge - FAQ", ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl(), new(0, 0, 5, 0)), "Shareable copied!", ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl()),
        null,
        GetImageUrl(Image.MoneyChallengeLogo),
        new(0, 0, 5, 0),
        ui.EnglishLocale[Content.MoneyChallengeFAQ]!.MinimalistUrl());
    public Shareable MoneyChallengeFAQ => _moneyChallengeFAQ;
}