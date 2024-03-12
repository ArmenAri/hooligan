using Hooligan.Presentation;

var builder = WebApplication.CreateBuilder(args);

{
    builder.AddServiceDefaults();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddProblemDetails();
    builder.Services.AddSwaggerGen();
    builder.Services.AddPresentation(builder.Configuration);
}

var app = builder.Build();

{
    app.UseExceptionHandler();
    app.MapDefaultEndpoints();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
}

app.Run();