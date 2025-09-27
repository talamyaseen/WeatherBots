using FluentAssertions;
using WeatherBots.Models;
using WeatherBots.Parsing;
using Xunit;

namespace WeatherBots.Tests
{
    public class JsonWeatherParserTests
    {
        [Fact]
        public void TryParse_ValidJson_ShouldReturnWeatherData()
        {
            var parser = new JsonWeatherParser();
            var json = "{ \"Location\": \"Test\", \"Temperature\": 25, \"Humidity\": 60 }";

            var result = parser.TryParse(json, out WeatherData? data);

            result.Should().BeTrue();
            data.Should().NotBeNull();
            data!.Location.Should().Be("Test");
            data.Temperature.Should().Be(25);
            data.Humidity.Should().Be(60);
        }

        [Fact]
        public void TryParse_InvalidJson_ShouldReturnFalse()
        {
            var parser = new JsonWeatherParser();
            var result = parser.TryParse("invalid", out var data);

            result.Should().BeFalse();
            data.Should().BeNull();
        }
    }
}
