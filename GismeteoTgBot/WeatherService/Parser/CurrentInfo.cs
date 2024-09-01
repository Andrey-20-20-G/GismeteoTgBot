using GismeteoTgBot.WeatherService.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace GismeteoTgBot.WeatherService.Parser
{
    public class CurrentInfo
    {
        private int[] ListOfElements = new int[8];

        public Task<List<DateTime>> GetCurrentTime(
            HtmlNodeCollection parserStringToday, HtmlNodeCollection parserStringTomorrow)
        {
            var currentTime = DateTime.Now;
            var listTime = new List<DateTime>();

            foreach (var time in parserStringToday)
            {
                var index = 1;
                while (index < 9)
                {
                    var parsedTime = Convert.ToDateTime(HtmlEntity.DeEntitize(
                        time.SelectSingleNode($"div[{index}]").InnerText));

                    if (currentTime.Hour < parsedTime.Hour)
                    {
                        ListOfElements[listTime.Count] = index;
                        listTime.Add(parsedTime);
                    }
                    index++;
                }
            }
            foreach (var time in parserStringTomorrow)
            {
                var index = 1;
                while(index < 9)
                {
                    var parsedTime = Convert.ToDateTime(HtmlEntity.DeEntitize(
                        time.SelectSingleNode($"div[{index}]").InnerText));

                    if (currentTime.Hour > parsedTime.Hour)
                    {
                        ListOfElements[listTime.Count] = index;
                        listTime.Add(parsedTime);
                    }
                    index++;
                }
            }
            return Task.FromResult(listTime);
        }

        public Task<List<string>> GetCurrentConditions(
            HtmlNodeCollection parserStringToday, HtmlNodeCollection parserStringTomorrow)
        {
            var listConditions = new List<string>();
            var index = 0;
            foreach (var condition in parserStringToday)
            {
                string parsedConditions;

                while (ListOfElements[index] != 8)
                {
                    parsedConditions = HtmlEntity.DeEntitize(
                        condition.SelectSingleNode($"div[{ListOfElements[index]}]").
                        GetAttributeValue("data-tooltip", "not found"));
                    listConditions.Add(parsedConditions);
                    index++;
                }
                parsedConditions = HtmlEntity.DeEntitize(
                    condition.SelectSingleNode($"div[{ListOfElements[index]}]").
                    GetAttributeValue("data-tooltip", "not found"));
                listConditions.Add(parsedConditions);
                index++;
            }
            foreach (var condition in parserStringTomorrow)
            {
                string parsedConditions;
                for (int i = index; i < 8; i++)
                {
                    parsedConditions = HtmlEntity.DeEntitize(
                        condition.SelectSingleNode($"div[{ListOfElements[i]}]").
                        GetAttributeValue("data-tooltip", "not found"));
                    listConditions.Add(parsedConditions);
                }
            }
            return Task.FromResult(listConditions);
        }
            

        public Task<List<int>> GetCurrentTemperature(
            HtmlNodeCollection parserStringToday, HtmlNodeCollection parserStringTomorrow)
        {
            var listTemperatures = new List<int>();
            var index = 0;

            foreach (var temp in parserStringToday)
            {
                int parsedTemp;
                while (ListOfElements[index] != 8)
                {
                    parsedTemp = Convert.ToInt32(HtmlEntity.DeEntitize(
                        temp.SelectSingleNode($"div[{ListOfElements[index]}]/temperature-value").
                        GetAttributeValue("value", "not found")));
                    listTemperatures.Add(parsedTemp);
                    index++;
                }
                parsedTemp = Convert.ToInt32(HtmlEntity.DeEntitize(
                    temp.SelectSingleNode($"div[{ListOfElements[index]}]/temperature-value").
                    GetAttributeValue("value", "not found")));
                listTemperatures.Add(parsedTemp);
                index++;
            }
            foreach (var temp in parserStringTomorrow)
            {
                for (int i = index; i < 8; i++)
                {
                    var parsedTemp = Convert.ToInt32(HtmlEntity.DeEntitize(
                        temp.SelectSingleNode($"div[{ListOfElements[i]}]/temperature-value").
                        GetAttributeValue("value", "not found")));
                    listTemperatures.Add(parsedTemp);
                }
            }
            return Task.FromResult(listTemperatures);
        }
    }
}
