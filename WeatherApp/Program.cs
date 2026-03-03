

using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Core;

var services = new ServiceCollection();

services.AddHttpClient<IWeatherService, WeatherService>();

var provider = services.BuildServiceProvider();

 var weatherService = provider.GetRequiredService<IWeatherService>();

Console.WriteLine("Enter city:");
var city = Console.ReadLine() ?? "Paris";

try
{
    var result = await weatherService.GetWeatherAsync(city,"");
    Console.WriteLine($"Weather in {city}: {result.TemperatureCelsius}°C, {result.Description}, {result.Country}");

}
catch (Exception ex)
{
    Console.WriteLine($"Error fetching weather: {ex.Message}");
}   




