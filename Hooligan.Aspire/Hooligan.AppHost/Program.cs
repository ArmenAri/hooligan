var builder = DistributedApplication.CreateBuilder(args);

var messaging = builder.AddRabbitMQ("messaging", 5300)
                    .WithEnvironment("RABBITMQ_DEFAULT_USER", "guest")
                    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "4g8BfAM6d623LAn8Rm8f");

var backend = builder.AddProject<Projects.Hooligan_Presentation>("backend")
                    .WithEnvironment("rabbitmqConnectionString", "amqp://guest:4g8BfAM6d623LAn8Rm8f@localhost:5300")
                    .WithReference(messaging);

var grpc = builder.AddProject<Projects.Hooligan_Grpc>("grpc")
                .WithEnvironment("rabbitmqConnectionString", "amqp://guest:4g8BfAM6d623LAn8Rm8f@localhost:5300")
                .WithReference(messaging);

builder.AddProject<Projects.Hooligan_Web>("front")
       .WithReference(grpc)
       .WithReference(backend);

builder.Build().Run();