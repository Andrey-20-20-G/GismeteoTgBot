using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GismeteoTgBot.WeatherService.Models
{
    public class WeatherModel
    {
        public int Temperature { get; set; }
        public string WeatherConditions { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
    }
}
