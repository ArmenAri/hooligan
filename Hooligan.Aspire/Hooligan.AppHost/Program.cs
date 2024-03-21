var builder = DistributedApplication.CreateBuilder(args);

var backend = builder.AddProject<Projects.Hooligan_Presentation>("backend");

var grpc = builder.AddProject<Projects.Hooligan_Grpc>("grpc");

builder.AddProject<Projects.Hooligan_Web>("front")
    .WithReference(grpc)
    .WithReference(backend);

builder.Build().Run();