using Grpc.Net.Client.Web;
using Hooligan.Web.Assembly;
using HooliganNotification;
using MudBlazor;
using MudBlazor.Services;
using App = Hooligan.Web.Components.App;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents()
                .AddInteractiveServerComponents();

builder.Services.AddOutputCache();

builder.Services.AddHttpClient<HooliganApiClient>(client => client.BaseAddress = new Uri("http://backend"));

builder.Services.AddGrpcClient<NotificationService.NotificationServiceClient>(options =>
    {
        options.Address = new Uri("http://grpc");
    })
    .ConfigurePrimaryHttpMessageHandler(() => new GrpcWebHandler(new HttpClientHandler()));

builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight; });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
else
{
    app.UseWebAssemblyDebugging();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Hooligan.Web.Assembly._Imports).Assembly);

app.MapDefaultEndpoints();

app.Run();