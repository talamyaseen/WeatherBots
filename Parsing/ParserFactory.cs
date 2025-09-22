using System;
using System.Collections.Generic;
using WeatherBots.Models;

namespace WeatherBots.Parsing
{
    public static class ParserFactory
    {
        private static readonly List<IWeatherParser> Parsers = new();

        static ParserFactory()
        {
            Parsers.Add(new JsonWeatherParser());
            Parsers.Add(new XmlWeatherParser());
        }

        public static bool TryParse(string input, out WeatherData? data)
        {
            data = null;
            var trimmed = input.TrimStart();

            //XML 
            if (trimmed.StartsWith("<"))
            {
                var xmlParser = Parsers.Find(p => p.Name == "xml");
                if (xmlParser != null && xmlParser.TryParse(input, out data)) return true;
            }
            //json
            else if (trimmed.StartsWith("{") || trimmed.StartsWith("["))
            {
                var jsonParser = Parsers.Find(p => p.Name == "json");
                if (jsonParser != null && jsonParser.TryParse(input, out data)) return true;
            }

            //fallback
            foreach (var p in Parsers)
            {
                if (p.TryParse(input, out data)) return true;
            }

            return false;
        }

        // Register other parsers
        public static void RegisterParser(IWeatherParser parser)
        {
            if (parser == null) throw new ArgumentNullException(nameof(parser));
            Parsers.Add(parser);
        }
    }
}
