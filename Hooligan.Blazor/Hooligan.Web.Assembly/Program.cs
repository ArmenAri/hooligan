using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Hooligan.Web.Assembly;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight; });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7259") });
builder.Services.AddScoped<HooliganApiClient>();

await builder.Build().RunAsync();
