using Hooligan.Presentation;

var HooliganSpecificOrigin = "_hooliganSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

{
    builder.AddServiceDefaults();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddControllers();
    builder.Services.AddProblemDetails();
    builder.Services.AddSwaggerGen();
    builder.Services.AddPresentation(builder.Configuration);

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: HooliganSpecificOrigin,
            policy =>
            {
                policy.WithOrigins("http://localhost:5044")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
    });
}

var app = builder.Build();

{
    app.UseExceptionHandler();
    app.MapDefaultEndpoints();
    app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors(HooliganSpecificOrigin);
}

app.Run();