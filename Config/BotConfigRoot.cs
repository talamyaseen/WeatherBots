using System.Text.Json.Serialization;

namespace WeatherBots.Config
{
    public class BotConfigRoot
    {
        [JsonPropertyName("RainBot")]
        public BotConfig? RainBot { get; set; }

        [JsonPropertyName("SunBot")]
        public BotConfig? SunBot { get; set; }

        [JsonPropertyName("SnowBot")]
        public BotConfig? SnowBot { get; set; }
    }

    public class BotConfig
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonPropertyName("humidityThreshold")]
        public double HumidityThreshold { get; set; }

        [JsonPropertyName("temperatureThreshold")]
        public double TemperatureThreshold { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
