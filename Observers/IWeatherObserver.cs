using WeatherBots.Models;

namespace WeatherBots.Observers
{
    public interface IWeatherObserver
    {
        string Name { get; }
        void Update(WeatherData data);
    }
}
