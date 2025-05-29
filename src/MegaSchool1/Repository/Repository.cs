using System.Net.Http.Json;
using System.Text.Json;
using Blazored.LocalStorage;
using MegaSchool1.Model;
using MegaSchool1.Model.API;
using MegaSchool1.Repository.Model;
using OneOf;
using OneOf.Types;

namespace MegaSchool1.Repository;

public class Repository(ILocalStorageService localStorage, HttpClient http)
{
    private const string SettingsKey = "settings";
    private const string UserDataKey = "user_data";
    private const string BackupKey = "backup";
    private const string ClientSettingsKey = "client_settings";

    public async Task<OneOf<GlobalData, None, Error<string>>> GetGlobalDataBackupAsync()
    {
        try
        {
            GlobalData? data = null;

            if (await localStorage.ContainKeyAsync(BackupKey))
            {
                data = await localStorage.GetItemAsync<GlobalData>(BackupKey);
            }

            if (data == null)
            {
                return default(None);
            }
            else
            {
                return data;
            }
        }
        catch (Exception ex)
        {
            return new Error<string>(ex.Message);
        }
    }

    public async Task<OneOf<Success, Error<string>>> SaveGlobalDataBackupAsync(GlobalData data)
    {
        try
        {
            await localStorage.SetItemAsync(BackupKey, data);
            return new Success();
        }
        catch (Exception ex)
        {
            return new Error<string>(ex.Message);
        }
    }

    public async Task<OneOf<Success, Error<string>>> SaveUserDataAsync(UserData userData)
    {
        try
        {
            await localStorage.SetItemAsync(UserDataKey, userData);
            return new Success();
        }
        catch (Exception ex)
        {
            return new Error<string>(ex.Message);
        }
    }

    public async Task<OneOf<UserData, None, Error<string>>> GetUserDataAsync()
    {
        try
        {
            if (await localStorage.ContainKeyAsync(UserDataKey))
            {
                var userData = await localStorage.GetItemAsync<UserData>(UserDataKey);
                return userData != null ? userData : new None();
            }
            else
            {
                return new None();
            }
        }
        catch (Exception ex)
        {
            return new Error<string>(ex.Message);
        }
    }

    public async Task<OneOf<Settings, None, Error<string>>> GetSettingsAsync()
    {
        try
        {
            if (await localStorage.ContainKeyAsync(SettingsKey))
            {
                var foundSettings = await localStorage.GetItemAsync<Settings>(SettingsKey);
                return foundSettings != null ? foundSettings : new None();
            }
            else
            {
                return new None();
            }
        }
        catch (Exception e)
        {
            return new Error<string>(e.Message);
        }
    }

    public async Task<OneOf<Success, Error<string>>> SaveGlobalDataAsync(GlobalData globalData)
    {
        try
        {
            await localStorage.SetItemAsync(SettingsKey, globalData);
            return new Success();
        }
        catch (Exception e)
        {
            return new Error<string>(e.Message);
        }
    }

    public async Task SaveSettingsAsync(Settings settings)
    {
        await localStorage.SetItemAsync(SettingsKey, settings);

    }

    public async Task<OneOf<ClientSettings, None, Error<string>>> GetClientSettingsAsync()
    {
        try
        {
            if (await localStorage.ContainKeyAsync(ClientSettingsKey))
            {
                var foundSettings = await localStorage.GetItemAsStringAsync(ClientSettingsKey);
                if(foundSettings == null)
                {
                    return new None();
                }
                else
                {
                    var deserialized = JsonSerializer.Deserialize<ClientSettings>(foundSettings, Util.JsonSerializerOptions);
                    return deserialized?.UI != null ? deserialized : new None();
                }
            }
            else
            {
                return new None();
            }
        }
        catch (Exception e)
        {
            return new Error<string>(e.Message);
        }
    }

    public async Task SaveClientSettingsAsync(ClientSettings settings)
    {
        var serialized = JsonSerializer.Serialize(settings, Util.JsonSerializerOptions);
        await localStorage.SetItemAsStringAsync(ClientSettingsKey, serialized);
    }

    public async Task<string?> GetUsernameAsync()
    {
        string? foundUsername = null;

        if (await localStorage.ContainKeyAsync(SettingsKey))
        {
            foundUsername = (await localStorage.GetItemAsync<Settings>(SettingsKey))?.User?.MemberId;

            // validate username
            if (string.IsNullOrWhiteSpace(foundUsername))
            {
                foundUsername = null;
            }
            else
            {
                if (!await Util.IsUsernameValidAsync(foundUsername, http))
                {
                    foundUsername = null;
                }
            }
        }

        return foundUsername;
    }
   
    public async Task<OneOf<QMD, None>> GetMarketingDirectorInfoAsync(string memberId)
    {
        try
        {
            var qmd = await http.GetFromJsonAsync<QMD>($"https://user.mwrfinancial.com/home/GetUsrFrmMwrMakeover?uName={memberId}");

            return qmd?.Email != null ? (OneOf<QMD, None>)qmd : default(None);
        }
        catch (Exception)
        {
        }

        return default(None);
    }
}