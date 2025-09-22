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
            return JsonSerializer.Deserialize<BotConfigRoot>(txt, opts) ?? new BotConfigRoot();
        }
    }
}
