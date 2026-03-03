using Microsoft.AspNetCore.Builder;
using WeatherApp.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IWeatherService, WeatherService>();
var app = builder.Build();

app.MapGet("/weather", async (string city, string country, IWeatherService weatherService) =>
{
    try
    {
        var weather = await weatherService.GetWeatherAsync(city, country);
        return Results.Ok(weather);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }

});
app.Run();
