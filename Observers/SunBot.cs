using System;
using WeatherBots.Config;
using WeatherBots.Models;

namespace WeatherBots.Observers
{
    public class SunBot : IWeatherObserver
    {
        private readonly BotConfig _cfg;
        public string Name => "SunBot";

        public SunBot(BotConfig cfg)
        {
            _cfg = cfg ?? throw new ArgumentNullException(nameof(cfg));
        }

        public void Update(WeatherData data)
        {
            if (!_cfg.Enabled) return;
            if (data.Temperature >= _cfg.TemperatureThreshold)
            {
                Console.WriteLine("SunBot activated!");
                Console.WriteLine($"SunBot: \"{_cfg.Message}\"");
            }
        }
    }
}
