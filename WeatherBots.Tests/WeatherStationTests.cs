using FluentAssertions;
using Moq;
using WeatherBots.Models;
using WeatherBots.Observers;
using Xunit;

namespace WeatherBots.Tests
{
    public class WeatherStationTests
    {
        [Fact]
        public void Attach_And_Publish_ShouldCallObserver()
        {
            var station = new WeatherStation();
            var mockObserver = new Mock<IWeatherObserver>();

            station.Attach(mockObserver.Object);

            var weather = new WeatherData { Location = "X", Temperature = 20, Humidity = 50 };
            station.Publish(weather);

            mockObserver.Verify(o => o.Update(weather), Times.Once);
        }

        [Fact]
        public void Detach_ShouldRemoveObserver()
        {
            var station = new WeatherStation();
            var mockObserver = new Mock<IWeatherObserver>();
            station.Attach(mockObserver.Object);

            station.Detach(mockObserver.Object);

            var weather = new WeatherData();
            station.Publish(weather);

            mockObserver.Verify(o => o.Update(It.IsAny<WeatherData>()), Times.Never);
        }
    }
}
