using GismeteoTgBot.WeatherService.Models;
using HtmlAgilityPack;
using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GismeteoTgBot.WeatherService.Parser
{
    public class WeatherParser : BackgroundService, IParserBase
    {
        public string Url { get; set; } = "https://www.gismeteo.ru/weather-omsk-4578/";
        public string City { get; set; }

        public WeatherInfo _info = new WeatherInfo();

        //public void SetUrl(string url, string city)
        //{
        //    if (City != null)
        //    {
        //        Url = url + "weather-" + city + "/";
        //    }
        //    else
        //    {
        //        Debug.WriteLine("City is empty");
        //    }

        //}

        //public void SetCity(string city)
        //{
        //    if (city == null)
        //    {
        //        Debug.WriteLine("City is empty");
        //    }
        //    else
        //    {
        //        City = city;
        //    }
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var web = new HtmlWeb();
            var doc = web.Load(Url);

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

            var list = await _info.GetWeatherInfo(listConditions, listTime, listTemperatures);
            await Task.Delay(100000, stoppingToken);
        }
    }
}

