using Hooligan.Grpc.Consumers;
using MassTransit;
using NotificationService = Hooligan.Grpc.Services.NotificationService;

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
        var connectionString = builder.Configuration.GetConnectionString("messaging");
        if (connectionString is not null)
        {
            cfg.Host(new Uri(connectionString));
        }
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
app.UseGrpcWeb();
app.MapGrpcService<NotificationService>().EnableGrpcWeb();

app.Run();
