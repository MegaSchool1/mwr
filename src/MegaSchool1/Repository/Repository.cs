using System.Net.Http.Json;
using Blazored.LocalStorage;
using MegaSchool1.Model;
using MegaSchool1.Model.API;
using MegaSchool1.Repository.Model;

namespace MegaSchool1.Repository;

public class Repository(ILocalStorageService localStorage, HttpClient http)
{
	private const string SettingsKey = "settings";
	private const string UserDataKey = "user_data";

	public async Task SaveUserDataAsync(UserData userData)
	{
		await localStorage.SetItemAsync(UserDataKey, userData);
	}

	public async Task<UserData?> GetUserDataAsync()
	{
		UserData? userData = null;

		if (await localStorage.ContainKeyAsync(UserDataKey))
		{
			userData = await localStorage.GetItemAsync<UserData>(UserDataKey);
		}

		return userData;
	}

	public async Task<Settings> GetSettingsAsync()
    {
        Settings? foundSettings = null;

	    if (await localStorage.ContainKeyAsync(SettingsKey))
	    {
		    foundSettings = await localStorage.GetItemAsync<Settings>(SettingsKey);
	    }

	    return foundSettings ?? new();
    }

	public async Task SaveSettingsAsync(Settings settings)
    {
        await localStorage.SetItemAsync(SettingsKey, settings);

    }

    public async Task<string?> GetUsernameAsync()
    {
        string? foundUsername = null;

        if (await localStorage.ContainKeyAsync(SettingsKey))
        {
            foundUsername = (await localStorage.GetItemAsync<Settings>(SettingsKey))?.Username;

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
   
    public async Task<QMD?> GetQMDInfo(string memberId)
    {
        QMD? qmd;

        qmd = await http.GetFromJsonAsync<QMD>($"https://user.mwrfinancial.com/home/GetUsrFrmMwrMakeover?uName={memberId}");

        return qmd;
    }
}