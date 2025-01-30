using MediatR;
using Products.Application.Categories.Queries.GetAllCategories;
using Products.Application.Extensions;
using Products.Infrastructure.Extensions;
using Products.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.AddSqlServerClient("sqlDb", configureSettings =>
{
    configureSettings.DisableHealthChecks = false;
    configureSettings.DisableTracing = false;
});

builder.Services.AddApplication();

builder.Services.AddInfrastructureServices(builder.Configuration);

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

// Map your API endpoints (Minimal API style)
app.MapGet("/api/categories", async (IMediator mediator) =>
{
    var categories = await mediator.Send(new GetAllCategoriesQuery());
    return Results.Ok(categories); // Use Results.Ok for Minimal APIs
});

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
