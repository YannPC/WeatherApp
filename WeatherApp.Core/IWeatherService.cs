using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public interface IWeatherService
    {
        Task<WeatherResult> GetWeatherAsync(string city, string country, CancellationToken cancellationToken = default);
    }
}
