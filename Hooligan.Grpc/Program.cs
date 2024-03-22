using Hooligan.Grpc.Consumers;
using Hooligan.Grpc.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<NotificationService>();
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AssociationConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration.GetConnectionString("messaging"));
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.UseGrpcWeb();
app.MapGrpcService<NotificationService>().EnableGrpcWeb();

app.Run();
