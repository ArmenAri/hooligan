using Hooligan.Grpc.Consumers;
using MassTransit;
using NotificationService = Hooligan.Grpc.Services.NotificationService;

var HooliganSpecificOrigin = "_hooliganSpecificOrigins";

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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
        });
});

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseCors();
app.UseRouting();

// Configure the HTTP request pipeline.
app.UseGrpcWeb();
app.MapGrpcService<NotificationService>().EnableGrpcWeb().RequireCors();

app.Run();
