using GismeteoTgBot.WeatherService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GismeteoTgBot.WeatherService.Interfaces
{
    public interface IWeatherBase
    {
        public List<WeatherModel>? WeatherListForDay { get; set; }

        public Task<string> GetWeatherInfo(
            List<string> weatherConditions, List<DateTime> time, List<int> temp);
    }
}
