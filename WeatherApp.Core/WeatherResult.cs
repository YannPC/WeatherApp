using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Core
{
    public class WeatherResult
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        public double TemperatureCelsius { get; set; }
        public string? Description { get; set; }
    }
}
