using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using WeatherApp.Core;
using Xunit;

namespace WeatherApp.Testing
{
    public class WeatherAppTest
    {
        [Fact]


        public async Task GetWeatherAsync_ReturnsMappedResult_WhenApiReturnsValidData()
        {

            // Arrange
            var json = "{\"Current_Weather\": {\"Temperature\": 25.5}}";
            using var httpClient = CreateHttpClientWithJson(json, HttpStatusCode.OK);
            var service = new WeatherService(httpClient);

            // Act
            var result = await service.GetWeatherAsync("Tokyo", "JP", CancellationToken.None);

            // Assert
            Assert.Equal("Tokyo", result.City);
            Assert.Equal("JP", result.Country);
            //Assert.Equal(25.5, result.TemperatureCelsius);
            Assert.Equal("Clear", result.Description);
        }

        private static HttpClient CreateHttpClientWithJson(string json, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handler = new FakeHttpMessageHandler(json, statusCode);
            return new HttpClient()
            {
                BaseAddress = new Uri("https://api.open-meteo.com/") // base not used by GetFromJsonAsync here but set for completeness
            };
        }

        [Fact]
        public async Task GetWeatherAsync_ThrowsWrappedException_WhenApiReturnsNullCurrentWeather()
        {
            // Arrange: API returns object with null Current_Weather which triggers an application exception inside the service
            var json = "{\"Current_Weather\": null}";
            using var httpClient = CreateHttpClientWithJson(json, HttpStatusCode.OK);
            var service = new WeatherService(httpClient);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => service.GetWeatherAsync("Tokyo", "JP", CancellationToken.None));
            Assert.NotNull(ex.InnerException);
            Assert.IsType<ApplicationException>(ex.InnerException);
            Assert.Contains("Failed to retrieve weather data", ex.InnerException.Message);
            
        }

        private sealed class FakeHttpMessageHandler
        {
            private readonly string _json;
            private readonly HttpStatusCode _statusCode;

            public FakeHttpMessageHandler(string json, HttpStatusCode statusCode)
            {
                _json = json;
                _statusCode = statusCode;
            }
        }


        //protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    var response = new HttpResponseMessage(_statusCode)
        //    {
        //        Content = new StringContent(_json, Encoding.UTF8, "application/json")
        //    };
        //    return Task.FromResult(response);
        //}

    }
}