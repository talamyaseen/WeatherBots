using System.Collections.Generic;
using WeatherBots.Config;

namespace WeatherBots.Observers
{
    public static class BotFactory
    {
        public static IEnumerable<IWeatherObserver> CreateBots(BotConfigRoot root)
        {
            var list = new List<IWeatherObserver>();

            if (root.RainBot is not null)
            {
                list.Add(new RainBot(root.RainBot));
            }

            if (root.SunBot is not null)
            {
                list.Add(new SunBot(root.SunBot));
            }

            if (root.SnowBot is not null)
            {
                list.Add(new SnowBot(root.SnowBot));
            }

            return list;
        }
    }
}
