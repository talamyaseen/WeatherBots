using System;
using WeatherBots.Config;
using WeatherBots.Models;

namespace WeatherBots.Observers
{
    public class RainBot : IWeatherObserver
    {
        private readonly BotConfig _cfg;
        public string Name => "RainBot";

        public RainBot(BotConfig cfg)
        {
            _cfg = cfg ?? throw new ArgumentNullException(nameof(cfg));
        }

        public void Update(WeatherData data)
        {
            if (!_cfg.Enabled) return;
            if (data.Humidity >= _cfg.HumidityThreshold)
            {
                Console.WriteLine("RainBot activated!");
                Console.WriteLine($"RainBot: \"{_cfg.Message}\"");
            }
        }
    }
}
