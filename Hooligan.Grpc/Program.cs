using Hooligan.Grpc.Consumers;
using MassTransit;
using NotificationService = Hooligan.Grpc.Services.NotificationService;

const string allowAll = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

{
    builder.AddServiceDefaults();

    // Add services to the container.
    builder.Services.AddGrpc();

    builder.Services.AddCors(o => o.AddPolicy(allowAll, policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:5044")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders(
                "Grpc-Status",
                "Grpc-Message",
                "Grpc-Encoding",
                "Grpc-Accept-Encoding");
    }));

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
}

var app = builder.Build();

{
    app.MapDefaultEndpoints();
    app.UseRouting();

    // Configure the HTTP request pipeline.
    app.UseGrpcWeb();
    app.UseCors();

    app.MapGrpcService<NotificationService>()
        .EnableGrpcWeb()
        .RequireCors(allowAll);
}

app.Run();