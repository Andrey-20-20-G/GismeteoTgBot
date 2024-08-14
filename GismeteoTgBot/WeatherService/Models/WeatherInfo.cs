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
        public string Weather = string.Empty;
        public List<WeatherModel>? WeatherListForDay { get; set; } = [];

        public async Task<string> GetWeatherInfo(
            List<string> weatherConditions, List<string> time, List<int> temp)
        {
            for (int i = 0; i < 8; i++)
            {
                if (weatherConditions[i] == null || time[i] == null)
                {
                    WeatherListForDay = null;
                    await Task.FromResult(Weather);
                }
                else
                {
                    WeatherListForDay?.Add(new WeatherModel
                    {
                        Temperature = temp[i],
                        WeatherConditions = weatherConditions[i],
                        Time = time[i],
                    });
                }
            }
            foreach (var day in WeatherListForDay)
            {
                Weather += $"{day.Time} | {day.WeatherConditions} | {day.Temperature} \n\n";
                Console.WriteLine($"{day.Time} | {day.WeatherConditions} | {day.Temperature}");
                Console.WriteLine($"__________________________________");
            }
            return await Task.FromResult(Weather);
        }

    }
}
