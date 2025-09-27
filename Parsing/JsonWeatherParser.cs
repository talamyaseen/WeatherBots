using System.Text.Json;
using WeatherBots.Models;

namespace WeatherBots.Parsing
{
    public class JsonWeatherParser : IWeatherParser
    {
        public string Name => "json";

        public bool TryParse(string input, out WeatherData? data)
        {
            data = null;
            try
            {
                var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                data = JsonSerializer.Deserialize<WeatherData>(input, opts);
                return data != null;
            }
            catch
            {
                return false;
            }
        }
    }
}
