using Append.Blazor.WebShare;
using Blazored.LocalStorage;
using MegaSchool1.Model;
using MegaSchool1.Repository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using MegaSchool1;
using Microsoft.AspNetCore.Components;
using Serilog;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Determine whether root components are already registered via prerednered HTML contents.
// See https://github.com/jsakamoto/BlazorWasmPreRendering.Build
if (!builder.RootComponents.Any())
{
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");
}

// Do not change this line, it's for prerendering. See https://github.com/jsakamoto/BlazorWasmPreRendering.Build
ConfigureServices(builder.Services, builder.HostEnvironment.BaseAddress, builder.Configuration);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Memory()
    .WriteTo.BrowserConsole()
    .CreateLogger();

var host = builder.Build();

// 👇 Invoke the "PreloadAsync" method of the "LazyLoader" service
//    to preload lazy assemblies needed for the current URL path before running.
var lazyLoader = host.Services.GetRequiredService<LazyLoader>();
await lazyLoader.PreloadAsync();

await host.RunAsync();

// This method signature follows a convention to enable prerendering.
// See https://github.com/jsakamoto/BlazorWasmPreRendering.Build
static void ConfigureServices(IServiceCollection services, string baseAddress, IConfiguration configuration)
{
    var http = new HttpClient { BaseAddress = new Uri(baseAddress) };
    services.AddScoped(sp => http);

    services.AddScoped<Repository>(sp => new(sp.GetRequiredService<ILocalStorageService>(), sp.GetRequiredService<HttpClient>()));

    var clientSettings = configuration.Get<ClientSettings>();
   
    services
        .AddSingleton(clientSettings!)
        .AddSingleton(sp => clientSettings?.UI ?? new())
        .AddSingleton(sp => new Constants(sp.GetRequiredService<UISettings>(), sp.GetRequiredService<NavigationManager>()))
        .AddSingleton(sp => new Mappers())
        .AddSingleton<LazyLoader>();

    services.AddMudServices();
    services.AddWebShare();
    services.AddBlazoredLocalStorage();
}