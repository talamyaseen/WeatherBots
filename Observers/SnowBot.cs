using System;
using WeatherBots.Config;
using WeatherBots.Models;

namespace WeatherBots.Observers
{
    public class SnowBot : IWeatherObserver
    {
        private readonly BotConfig _cfg;
        public string Name => "SnowBot";

        public SnowBot(BotConfig cfg)
        {
            _cfg = cfg ?? throw new ArgumentNullException(nameof(cfg));
        }

        public void Update(WeatherData data)
        {
            if (!_cfg.Enabled) return;
            if (data.Temperature <= _cfg.TemperatureThreshold)
            {
                Console.WriteLine("SnowBot activated!");
                Console.WriteLine($"SnowBot: \"{_cfg.Message}\"");
            }
        }
    }
}
