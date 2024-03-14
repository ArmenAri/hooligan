var builder = DistributedApplication.CreateBuilder(args);

var backend = builder
    .AddProject<Projects.Hooligan_Presentation>("backend");

builder.AddProject<Projects.Hooligan_Web>("front")
    .WithReference(backend);

builder.Build().Run();