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
        public string Url { get; set; } = "https://www.gismeteo.ru/weather-omsk-4578/";
        public string City { get; set; }
        
        public WeatherParser()
        {
            _info = new WeatherInfo();
            _tgSettings = new TgSettingsModel();
        }

        public async Task<string> GetWeatherListForDay()
        {
            var web = new HtmlWeb();
            var doc = web.Load("https://www.gismeteo.ru/weather-omsk-4578/");  //_tgSettings.Url

            var counter = 1;
            var checker = false;

            var conditions = doc.
                DocumentNode.SelectNodes(
                "/html/body/main/div[1]/section[3]/div[1]/div/div/div[position()>1 and position()<4]");
            //var conditions = doc.
            //    DocumentNode.SelectNodes(
            //    "/html/body/main/div[1]/section[3]/div[1]/div/div/div[3]/div[1]");
            var temperatures = doc.
                DocumentNode.SelectNodes(
                "/html/body/main/div[1]/section[3]/div[1]/div/div/div[4]/div/div");

            var listConditions = new List<string>();
            var listTime = new List<string>();
            var listTemperatures = new List<int>();

            foreach (var condition in conditions)
            {
                if (!checker)
                {
                    while (counter < 9)
                    {
                        listTime.Add(HtmlEntity.DeEntitize(
                        condition.SelectSingleNode($"div[{counter}]").InnerText));
                        counter++;
                    }
                }
                else
                {
                    while (counter < 9)
                    {
                        listConditions.Add(HtmlEntity.DeEntitize(
                        condition.SelectSingleNode($"div[{counter}]").GetAttributeValue("data-tooltip", "not found")));
                        counter++;
                    }
                }
                counter = 1;
                checker = !checker;
            }

            foreach (var temperature in temperatures)
            {
                while (counter < 9)
                {

                    listTemperatures.Add(Convert.ToInt32(HtmlEntity.DeEntitize(
                    temperature.SelectSingleNode($"div[{counter}]/temperature-value").GetAttributeValue("value", "not found"))));
                    counter++;
                }
            }

            return await _info.GetWeatherInfo(listConditions, listTime, listTemperatures);
        }
    }
}

