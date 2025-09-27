using FluentAssertions;
using System.IO;
using System.Text.Json;
using WeatherBots.Config;
using WeatherBots.Observers;
using Xunit;


namespace WeatherBots.Tests
{
    public class ConfigLoaderTests
    {
        [Fact]
        public void Load_WithValidJsonFile_ReturnsPopulatedBotConfigRoot()
        {
            // Arrange
            var json = "{\"RainBot\": { \"enabled\": true, \"humidityThreshold\": 70, \"message\": \"It looks like it's about to pour down!\" }," +
                " \"SunBot\": { \"enabled\": true, \"temperatureThreshold\": 30, \"message\": \"Scorching!\" }}";
            var tmp = Path.GetTempFileName();
            File.WriteAllText(tmp, json);

            // Act
            var root = ConfigLoader.Load(tmp);

            // Assert
            root.Should().NotBeNull();
            root.RainBot.Should().NotBeNull();
            root.RainBot!.HumidityThreshold.Should().Be(70);
            root.SunBot.Should().NotBeNull();

            // Cleanup
            File.Delete(tmp);
        }


        [Fact]
        public void Load_NonDeserializable_ThrowsInvalidDataException()
        {
            var tmp = Path.GetTempFileName();
            File.WriteAllText(tmp, "not a json");

            // Act
            System.Action act = () => ConfigLoader.Load(tmp);

            // Assert
            act.Should().Throw<JsonException>();

            File.Delete(tmp);
        }

        [Fact]
        public void Load_EmptyFile_ThrowsInvalidDataException()
        {
            var tmp = Path.GetTempFileName();
            File.WriteAllText(tmp, string.Empty);

            Action act = () => ConfigLoader.Load(tmp);

            act.Should().Throw<JsonException>();
            File.Delete(tmp);
        }

    }
}