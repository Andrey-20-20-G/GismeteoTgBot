using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GismeteoTgBot.WeatherService.Parser
{
    public interface IParserBase
    {
        public string Url { get; set; }
        public string City { get; set; }

        //public void SetUrl(string url, string city);

    }
}
