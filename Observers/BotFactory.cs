using System.Collections.Generic;
using WeatherBots.Config;

namespace WeatherBots.Observers
{
    public static class BotFactory
    {
        public static IEnumerable<IWeatherObserver> CreateBots(BotConfigRoot root)
        {
            var list = new List<IWeatherObserver>();

            if (root.RainBot != null)
            {
                list.Add(new RainBot(root.RainBot));
            }

            if (root.SunBot != null)
            {
                list.Add(new SunBot(root.SunBot));
            }

            if (root.SnowBot != null)
            {
                list.Add(new SnowBot(root.SnowBot));
            }

            return list;
        }
    }
}
