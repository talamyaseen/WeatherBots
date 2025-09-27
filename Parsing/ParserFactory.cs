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
            foreach (var parser in Parsers)
            {
                if (parser.TryParse(trimmed, out data)) return true;
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
