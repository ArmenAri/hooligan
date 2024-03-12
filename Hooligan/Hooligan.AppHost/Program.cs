var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Hooligan_ApiService>("apiservice");

builder.AddProject<Projects.Hooligan_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();