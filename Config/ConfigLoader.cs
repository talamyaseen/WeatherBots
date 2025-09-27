using System.IO;
using System.Text.Json;

namespace WeatherBots.Config
{
    public static class ConfigLoader
    {
        public static BotConfigRoot Load(string path)
        {
            var txt = File.ReadAllText(path);
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var config = JsonSerializer.Deserialize<BotConfigRoot>(txt, opts);
            if (config is null)
                throw new InvalidDataException($"Could not deserialize the config file at path: {path}"); 
            return config;
        }
    }
}
