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

        public Task<List<WeatherModel>>? GetWeatherInfo(
            List<string> weatherConditions, List<string> time, List<int> temp);

    }

}
