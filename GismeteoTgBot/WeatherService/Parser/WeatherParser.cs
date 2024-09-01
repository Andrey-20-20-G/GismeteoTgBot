using GismeteoTgBot.WeatherService.Models;
using HtmlAgilityPack;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GismeteoTgBot.BotSettings.Interfaces;
using GismeteoTgBot.WeatherService.Interfaces;
using GismeteoTgBot.BotSettings.Models;

namespace GismeteoTgBot.WeatherService.Parser
{
    public class WeatherParser
    {
        private readonly ITgSettingsBase _tgSettings;
        private readonly IWeatherBase _info;
        private CurrentInfo _currentInfo;
        public string Url { get; set; } = "https://www.gismeteo.ru/weather-omsk-4578/";
        public string City { get; set; }
        
        public WeatherParser()
        {
            _info = new WeatherInfo();
            _tgSettings = new TgSettingsModel();
            _currentInfo = new CurrentInfo();
        }

        public async Task<string> GetWeatherListForDay()
        {
            var web = new HtmlWeb();
            var docToday = web.Load("https://www.gismeteo.ru/weather-omsk-4578/");
            var docTomorrow = web.Load("https://www.gismeteo.ru/weather-omsk-4578/tomorrow/");//_tgSettings.Url

            var times = docToday.
                DocumentNode.SelectNodes(
                "/html/body/main/div[1]/section[3]/div[1]/div/div/div[2]");
            var conditions = docToday.
                DocumentNode.SelectNodes(
                "/html/body/main/div[1]/section[3]/div[1]/div/div/div[3]");
            var temperatures = docToday.
                DocumentNode.SelectNodes(
                "/html/body/main/div[1]/section[3]/div[1]/div/div/div[4]/div/div");

            var timesTomorrow = docTomorrow.
                DocumentNode.SelectNodes(
                "/html/body/main/div[1]/section[3]/div[1]/div/div/div[2]");
            var conditionsTomorrow = docTomorrow.
                DocumentNode.SelectNodes(
                "/html/body/main/div[1]/section[3]/div[1]/div/div/div[3]");
            var temperaturesTomorrow = docTomorrow.
                DocumentNode.SelectNodes(
                "/html/body/main/div[1]/section[3]/div[1]/div/div/div[4]/div/div");

            var listTime = await _currentInfo.GetCurrentTime(times, timesTomorrow);
            var listConditions = await _currentInfo.GetCurrentConditions(conditions, conditionsTomorrow);
            var listTemperatures = await _currentInfo.GetCurrentTemperature(temperatures, temperaturesTomorrow);

            return await _info.GetWeatherInfo(listConditions, listTime, listTemperatures);
        }

        
    }
}

