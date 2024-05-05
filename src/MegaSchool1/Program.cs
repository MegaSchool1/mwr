using Append.Blazor.WebShare;
using Blazored.LocalStorage;
using MegaSchool1.Model;
using MegaSchool1.Repository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using MegaSchool1;
using Microsoft.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => http);

builder.Services.AddScoped<Repository>(sp => new(sp.GetRequiredService<ILocalStorageService>(), sp.GetRequiredService<HttpClient>()));

var clientSettings = await http.GetFromJsonAsync<ClientSettings>("appsettings.json", new System.Text.Json.JsonSerializerOptions() { Converters = { new JsonStringEnumConverter() } });

builder.Services.AddSingleton(sp =>
{
    return clientSettings?.UI ?? new();
});

builder.Services.AddSingleton(sp =>
{
    return new Constants(sp.GetRequiredService<UISettings>(), sp.GetRequiredService<NavigationManager>());
});

builder.Services.AddMudServices();
builder.Services.AddWebShare();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
