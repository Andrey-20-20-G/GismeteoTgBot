using GismeteoTgBot.WeatherService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GismeteoTgBot.WeatherService.Models
{
    public class WeatherInfo : IWeatherBase
    {
        public List<WeatherModel> WeatherListForDay { get; set; } = [];

        public async Task<List<WeatherModel>>? GetWeatherInfo(
            List<string> weatherConditions, List<string> time, List<int> temp)
        {
            for (int i = 0; i < 8; i++)
            {
                if (weatherConditions[i] == null || time[i] == null)
                {
                    WeatherListForDay = null;
                    return WeatherListForDay;
                }
                else
                {
                    WeatherListForDay.Add(new WeatherModel
                    {
                        Temperature = temp[i],
                        WeatherConditions = weatherConditions[i],
                        Time = time[i],
                    });
                }
            }
            foreach (var day in WeatherListForDay)
            {
                Console.WriteLine($"{day.Time} | {day.WeatherConditions} | {day.Temperature}");
                Console.WriteLine($"__________________________________");
            }
            return WeatherListForDay;
        }

    }
}
