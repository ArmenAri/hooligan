using Hooligan.Presentation;

const string allowAll = "AllowAll";

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
        options.AddPolicy(allowAll,
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
    app.UseCors(allowAll);
}

app.Run();