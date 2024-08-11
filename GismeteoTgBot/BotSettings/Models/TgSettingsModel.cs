using GismeteoTgBot.BotSettings.Interfaces;
using GismeteoTgBot.WeatherService.Parser;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GismeteoTgBot.BotSettings.Models
{
    public class TgSettingsModel : ITgSettingsBase
    {
        public string Url { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public void SetCity(CityEnum city)
        {
            if (city == CityEnum.None)
            {
                Debug.WriteLine("City is empty");
            }
            else
            {
                City = city;
            }
        }

        public void SetUrl(string url, CityEnum city)
        {
            if (City != null)
            {
                Url = url + "weather-" + city + "/";
            }
            else
            {
                Debug.WriteLine("City is empty");
            }
        }
    }
}
