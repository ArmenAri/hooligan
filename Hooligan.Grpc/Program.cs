using Hooligan.Infrastructure.GrpcService;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<NotificationService>();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.UseGrpcWeb();
app.MapGrpcService<NotificationService>().EnableGrpcWeb();

app.Run();
