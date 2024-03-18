var builder = DistributedApplication.CreateBuilder(args);

var messaging = builder.AddRabbitMQ("messaging");

var backend = builder.AddProject<Projects.Hooligan_Presentation>("backend")
                     .WithReference(messaging);

var grpc = builder.AddProject<Projects.Hooligan_Grpc>("grpc")
                  .WithReference(messaging);

builder.AddProject<Projects.Hooligan_Web>("front")
       .WithReference(grpc)
       .WithReference(backend);

builder.Build().Run();