using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Hooligan.Web.Assembly;
using HooliganNotification;
using MudBlazor;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight; });

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7259") });
builder.Services.AddScoped<HooliganApiClient>();

builder.Services.AddGrpcClient<NotificationService.NotificationServiceClient>(options =>
    {
        options.Address = new Uri("http://localhost:5136");
    })
    .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

await builder.Build().RunAsync();
