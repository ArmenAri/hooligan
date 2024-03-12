var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.Hooligan_Presentation>("api");

builder.AddProject<Projects.Hooligan_Web>("front")
    .WithReference(api);

builder.Build().Run();