using MegaSchool1.Model.API;
using MegaSchool1.Model.Repository;
using OneOf;
using OneOf.Types;
using System.Net.Http.Json;
using System.Runtime;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace MegaSchool1.Model;

public static class Util
{
    private static readonly Regex ValidGivBuxCode = new(@"^([a-z]|\d)+$");

    public static string? ValidateGivBuxCode(string givBuxCode)
    {
        if (!string.IsNullOrWhiteSpace(givBuxCode))
        {
            var valid = ValidGivBuxCode.IsMatch(givBuxCode);
            if (valid)
            {
                return null;
            }
        }

        return "GivBux code must be all lower case and NO spaces!";
    }

    public static TeamMember Sanitize(TeamMember teamMember, OneOf<QMD, None> marketingDirector, string? givBuxCode)
    {
        var sanitized = teamMember;

        // name
        if(string.IsNullOrWhiteSpace(teamMember.Name))
        {
            sanitized = sanitized with 
            {
                Name = marketingDirector.IsT1 ? teamMember.MemberId : (marketingDirector.AsT0.BusnmShow ? marketingDirector.AsT0.BusinessName : $"{marketingDirector.AsT0.FirstName} {marketingDirector.AsT0.FirstName}")
            };
        }

        // GivBux code
        if (string.IsNullOrWhiteSpace(teamMember.GivBuxCode))
        {
            sanitized = sanitized with { GivBuxCode = givBuxCode };
        }
     
        return sanitized;
    }

    //public record MemberIdUnchanged() { public string Message => $"Member ID must be different than current value"; };
    public record MemberDoesNotExist(string MemberId) { public string Message => $"'{MemberId}' does not exist.  Tip: If your website is '{Constants.MarketingDirectorUrlEnglish("ScoobyDoo")}' then your username is 'ScoobyDoo'"; };
    public record MemberIdNotSet() { public string Message => $"'Member ID must have a value"; };

    public static OneOf<MemberDoesNotExist, MemberIdNotSet, None> ValidateMemberIdChange(string oldMemberId, (string? MemberId, OneOf<QMD, None> Info) newMember)
    {
        // member ID is set
        if(string.IsNullOrWhiteSpace(newMember.MemberId))
        {
            return new MemberIdNotSet();
        }

        //// member ID is different
        //if(string.Equals(oldMemberId, newMember.MemberId, StringComparison.InvariantCultureIgnoreCase))
        //{
        //    return new MemberIdUnchanged();
        //}

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

    public static int MinuteEstimate(TimeSpan duration) => duration.Minutes + (duration.Seconds >= 30 ? 1 : 0);

    private static readonly TimeZoneInfo[] BusinessStandardTimeZonesOrdered = [Constants.DefaultTimeZone, Constants.ChicagoTimeZone, Constants.LosAngelesTimeZone];

    public static string GetRegionalTimes(DateTimeOffset dateTime)
    {
        var regionalTimes = BusinessStandardTimeZonesOrdered
            .Select(timeZone => $"{dateTime.ToOffset(timeZone.BaseUtcOffset).ToString("h:mmt").ToLower()}{timeZone.StandardName.First()}{timeZone.StandardName.Substring(1).ToLower()}")
            .Aggregate((formatted, next) => $"{formatted}/ {next}");

        return regionalTimes;
    }
}