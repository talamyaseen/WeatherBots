using System.Collections.Generic;
using WeatherBots.Models;

namespace WeatherBots.Observers
{
    public class WeatherStation
    {
        private readonly List<IWeatherObserver> _observers = new();

        public void Attach(IWeatherObserver observer)
        {
            if (!_observers.Contains(observer)) _observers.Add(observer);
        }

        public void Detach(IWeatherObserver observer)
        {
            if (_observers.Contains(observer)) _observers.Remove(observer);
        }

        public void Publish(WeatherData data)
        {
            foreach (var o in _observers)
            {
                    o.Update(data);
            }
        }
    }
}
