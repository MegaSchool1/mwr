using MegaSchool1.Model.API;
using MegaSchool1.Model.Repository;
using MegaSchool1.Repository.Model;
using OneOf;
using OneOf.Types;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Web;
using MegaSchool1.Model.Dto;
using System.Text.Json;
using Foundation.Model;

namespace MegaSchool1.Model;

public static class Util
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new() { Converters = { new UnknownEnumConverter() } };

    public static string? ValidateGivBuxCode(string givBuxCode)
    {
        // allow null or empty
        if (string.IsNullOrEmpty(givBuxCode))
        {
            return null;
        }
        else
        {
            // allow all lower case w/ no white space
            if (!string.IsNullOrWhiteSpace(givBuxCode))
            {
                if (givBuxCode == givBuxCode.ToLower())
                {
                    return null;
                }
            }
        }

        return "GivBux code must be all lower case and NO spaces!";
    }

    public static async Task<Settings> SanitizeAsync(Settings settings, Func<string, Task<OneOf<QMD,None>>> marketingDirector)
    {
        var sanitized = settings with { };

        if (settings.User != null)
        {
            var marketingDirectorResult = await marketingDirector(settings.User.MemberId);

            sanitized.User = Sanitize(settings.User, marketingDirectorResult, settings.User.GivBuxCode);
        }
        else if (!string.IsNullOrWhiteSpace(settings.Username))
        {
            var marketingDirectorResult = await marketingDirector(settings.Username);

            sanitized.User = Sanitize(new TeamMember() with { MemberId = settings.Username }, marketingDirectorResult, settings.GivBuxCode);
        }

        // sanitize team members' info
        var sanitizedTeamMembers = new List<TeamMember>();
        foreach (var teamMember in settings.TeamMembers)
        {
            var marketingDirectorResult = await marketingDirector(teamMember.MemberId);

            sanitizedTeamMembers.Add(Sanitize(teamMember, marketingDirectorResult, teamMember.GivBuxCode));
        }

        sanitized.TeamMembers = sanitizedTeamMembers;

        return sanitized;
    }

    public static TeamMember Sanitize(TeamMember teamMember, OneOf<QMD, None> marketingDirector, string? givBuxCode)
    {
        var sanitized = teamMember;

        // name
        if(string.IsNullOrWhiteSpace(teamMember.Name))
        {
            sanitized = sanitized with 
            {
                Name = marketingDirector.IsT1 ? teamMember.MemberId : (marketingDirector.AsT0.BusnmShow ? marketingDirector.AsT0.BusinessName : $"{marketingDirector.AsT0.FirstName} {marketingDirector.AsT0.LastName}")
            };
        }

        // GivBux code
        var sanitizedGivBuxCode = HttpUtility.UrlEncode(!string.IsNullOrWhiteSpace(teamMember.GivBuxCode) ? teamMember.GivBuxCode : givBuxCode);
        sanitized = sanitized with { GivBuxCode = sanitizedGivBuxCode };
     
        return sanitized;
    }

    public record MemberDoesNotExist(string MemberId) { public string Message => $"'{MemberId}' does not exist.  Tip: If your website is '{Constants.MarketingDirectorUrlEnglish("ScoobyDoo")}' then your username is 'ScoobyDoo'"; };
    public record MemberIdNotSet() { public string Message => $"'Member ID must have a value"; };

    public static OneOf<MemberDoesNotExist, MemberIdNotSet, None> ValidateMemberIdChange(string oldMemberId, (string? MemberId, OneOf<QMD, None> Info) newMember)
    {
        // member ID is set
        if(string.IsNullOrWhiteSpace(newMember.MemberId))
        {
            return new MemberIdNotSet();
        }

        // member exists
        if(newMember.Info.IsT1)
        {
            return new MemberDoesNotExist(newMember.MemberId);
        }

        // valid member ID change
        return default(None);
    }

    public static async Task<bool> IsUsernameValidAsync(string username, HttpClient http)
    {
        // check if username belongs to an active Qualified Marketing Director (QMD)
        var userBusinessEnrollmentPage = await GetAsync(Constants.BusinessEnrollmentUrl(username), http);

        return userBusinessEnrollmentPage?.IsSuccessStatusCode is true;
    }

    public static async Task<bool> UpdateAvailableAsync(HttpClient http)
    {
        var currentVersion = typeof(Util).Assembly.GetName().Version;

        var clientSettings = await http.GetFromJsonAsync<ClientSettings>("appsettings.json", new System.Text.Json.JsonSerializerOptions() { Converters = { new JsonStringEnumConverter() } });

        if (clientSettings != null)
        {
            var latestVersion = Version.Parse(clientSettings.LatestVersion);

            return currentVersion == null || latestVersion > currentVersion;

        }
        else
        {
            return false;
        }
    }

    /// <summary>
    ///     Make a URL bypass the browser cache by faking a unique request.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string MakeUrlBypassBrowserCache(string url) => $"{url}?v={Guid.NewGuid()}";

    private static async Task<HttpResponseMessage?> GetAsync(string url, HttpClient http)
    {
        HttpResponseMessage? response = null;

        try
        {
            response = await http.GetAsync(url);
        }
        catch (Exception) { }

        return response;
    }

    public static int GetMonthlyIncome(int teamMembers)
    {
        return Constants.DailyGuarantee.Reverse().First(x => x.Value.NumMemberships <= teamMembers).Value.MonthlyPay;
    }

    public static TeamLevel[] GetTeamLevels(int monthlyIncomeGoal, TeamLevel[] distribution)
    {
        if (monthlyIncomeGoal == 0)
        {
            return Array.Empty<TeamLevel>();
        }
        else
        {
            var numMembershipToMeetMonthlyIncomeGoal = GetNumMembershipsToFundMonthlyBill(monthlyIncomeGoal);
            List<TeamLevel> teamLevels;

            var minimumRequiredLevel = distribution.FirstOrDefault(teamLevel => teamLevel.TeamMembersTotal >= numMembershipToMeetMonthlyIncomeGoal);
            if (minimumRequiredLevel == null)
            {
                teamLevels = [new("N/A", 0, null)];
            }
            else
            {
                teamLevels = distribution.Where(teamLevel => teamLevel.TeamMembersTotal <= minimumRequiredLevel.TeamMembersTotal).ToList();
            }

            return teamLevels.OrderBy(x => teamLevels.IndexOf(x)).ToArray();
        }
    }

    public static int GetNumMembershipsToFundMonthlyBill(int monthlyBillAmount)
    {
        var minimumRankToPayMonthlyBill = GetRankForMonthlyIncome(monthlyBillAmount);

        return minimumRankToPayMonthlyBill != Rank.None ? Constants.DailyGuarantee[minimumRankToPayMonthlyBill].NumMemberships : 0;
    }

    public static Rank GetRankForMonthlyIncome(int monthlyIncome)
        => Constants.DailyGuarantee.First(x => x.Value.MonthlyPay >= monthlyIncome).Key;

    public static TimeSpan MinuteEstimate(TimeSpan duration)
    {
        var minimumAllowableTime = TimeSpan.FromMinutes(1);
        var minutesToAdd = TimeSpan.FromMinutes(duration.Seconds >= 30 ? 1.0 : 0.0);
        var secondsToSubtract = minutesToAdd > TimeSpan.Zero ? TimeSpan.FromSeconds(duration.Seconds) : TimeSpan.Zero;
        var estimatedTime = duration.Add(minutesToAdd).Subtract(secondsToSubtract);
        
        return estimatedTime > minimumAllowableTime ? estimatedTime : minimumAllowableTime;
    }

    private static readonly TimeZoneInfo[] BusinessStandardTimeZonesOrdered = [Constants.DefaultTimeZone, Constants.ChicagoTimeZone, Constants.LosAngelesTimeZone];

    public static string GetRegionalTimes(DateTimeOffset dateTime)
    {
        var regionalTimes = BusinessStandardTimeZonesOrdered
            .Select(timeZone => $"{dateTime.ToOffset(timeZone.BaseUtcOffset).ToString("h:mmt").ToLower()}{timeZone.StandardName.First()}{timeZone.StandardName.Substring(1).ToLower()}")
            .Aggregate((formatted, next) => $"{formatted}/ {next}");

        return regionalTimes;
    }
}