using System.Net.Http.Json;

namespace WeatherApp.Core
{
    public class WeatherService: IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResult> GetWeatherAsync(string city, string country, CancellationToken cancellationToken = default)
        {
            try
            {
                var url = "https://api.open-meteo.com/v1/forecast?latitude=35.6895&longitude=139.6917&current_weather=true";

                var response = await _httpClient.GetFromJsonAsync<OpenMeteoResponse>(url, cancellationToken);


                if (response?.Current_Weather == null)
               
                    throw new ApplicationException("Failed to retrieve weather data.");

                return new WeatherResult
                {

                    City = city,

                    Country = country,

                    TemperatureCelsius = response.Current_Weather.Temperature,

                    Description = "Clear"
                };

            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception($"Failed to get weather data for Country {country}, City {city}.", ex);

            }

        }
        public class OpenMeteoResponse
        {
            public CurrentWeather? Current_Weather { get; set; }
        }

        public class CurrentWeather
        { 
            public double Temperature { get; set; }
        }
    }
}
