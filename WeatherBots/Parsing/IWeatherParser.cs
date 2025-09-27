using WeatherBots.Models;

namespace WeatherBots.Parsing
{
    public interface IWeatherParser
    {
        string Name { get; }
        bool TryParse(string input, out WeatherData? data);
    }
}
