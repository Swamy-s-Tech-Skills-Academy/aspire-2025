using AspireApp.ApiService.Models;
using AspireApp.ApiService.Persistence;
using AspireApp.ServiceDefaults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

builder.AddSqlServerClient("sqldb");

builder.AddSqlServerDbContext<WeatherDbContext>("sqldb");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

#region ADO.NET SQL Connection
using (var scope = app.Services.CreateScope())
{
    var connection = scope.ServiceProvider.GetRequiredService<SqlConnection>();
    connection.Open();

    // Ensure the database exists
    var createDbCommand = new SqlCommand(@"
            IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'WeatherDB')
            BEGIN
                  CREATE DATABASE WeatherDB;
            END;", connection);

    createDbCommand.ExecuteNonQuery();

    // Ensure the table exists
    var createTableCommand = new SqlCommand(@"
            USE WeatherDB;
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='WeatherForecasts' and xtype='U')
            CREATE TABLE WeatherForecasts (
                Id INT PRIMARY KEY IDENTITY,
                Date DATE NOT NULL,
                TemperatureC INT NOT NULL,
                Summary NVARCHAR(100) NOT NULL
            )", connection);

    createTableCommand.ExecuteNonQuery();

    // Check if data exists
    var checkDataCommand = new SqlCommand("SELECT COUNT(*) FROM WeatherForecasts", connection);
    var count = (int)checkDataCommand.ExecuteScalar();

    if (count == 0)
    {
        foreach (var index in Enumerable.Range(1, 5))
        {
            var date = DateTime.Now.AddDays(index).Date;
            var temperatureC = Random.Shared.Next(-20, 55);
            var summary = summaries[Random.Shared.Next(summaries.Length)];

            var insertCommand = new SqlCommand(@"
                    INSERT INTO WeatherForecasts (Date, TemperatureC, Summary)
                    VALUES (@Date, @TemperatureC, @Summary)", connection);

            insertCommand.Parameters.AddWithValue("@Date", date);
            insertCommand.Parameters.AddWithValue("@TemperatureC", temperatureC);
            insertCommand.Parameters.AddWithValue("@Summary", summary);

            insertCommand.ExecuteNonQuery();
        }
    }
}

app.MapGet("/weatherforecast", ([FromServices] SqlConnection connection) =>
{
    connection.Open();

    var command = new SqlCommand(@"
    USE WeatherDB;
    SELECT Date, TemperatureC, Summary FROM WeatherForecasts", connection);
    var weatherForecasts = new List<WeatherForecast>();

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        weatherForecasts.Add(new WeatherForecast
        {
            Date = DateOnly.FromDateTime(reader.GetDateTime(0)),
            TemperatureC = reader.GetInt32(1),
            Summary = reader.GetString(2)
        });
    }

    return weatherForecasts.ToArray();
});
#endregion

#region EF Core SQL Connection
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
    context.Database.EnsureCreated();

    if (!context.WeatherForecasts.Any())
    {
        foreach (var index in Enumerable.Range(1, 5))
        {
            context.WeatherForecasts.Add(new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            });

            context.SaveChanges();
        }
    }
}

app.MapGet("/weatherforecastv2", ([FromServices] WeatherDbContext context) =>
{
    return context.WeatherForecasts.ToArray();
});
#endregion

#region Dummy Data
app.MapGet("/weatherforecastv1", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecastV1
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});
#endregion

app.MapDefaultEndpoints();

app.Run();

record WeatherForecastV1(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
