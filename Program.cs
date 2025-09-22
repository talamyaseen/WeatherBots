using System;
using System.IO;
using WeatherBots.Config;
using WeatherBots.Observers;
using WeatherBots.Parsing;
using WeatherBots.Utils;

namespace WeatherBots
{
    internal class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Console.WriteLine("WeatherBots — Starting up...");

                // ??? ?? ????? ????? ????? ??? exe
                var configPath = Path.Combine(AppContext.BaseDirectory, "bots_config.json");
                if (!File.Exists(configPath))
                {
                    Console.WriteLine("Config file not found: bots_config.json");
                    return 1;
                }

                Console.WriteLine($"Using config file: {Path.GetFullPath(configPath)}");
                var config = ConfigLoader.Load(configPath);

                var station = new WeatherStation();
                foreach (var bot in BotFactory.CreateBots(config))
                {
                    station.Attach(bot);
                    Console.WriteLine($"Attached bot: {bot.Name}");
                }

                Console.WriteLine("Enter weather data (JSON or XML), or 'exit' to quit.");
                Console.WriteLine("Paste JSON or XML, multi-line supported. Type 'exit' to quit.");

                while (true)
                {
                    Console.Write("\nEnter weather data: ");
                    var raw = InputReader.ReadDocument();
                    if (raw == null) break;

                    raw = raw.Trim();
                    if (string.Equals(raw, "exit", StringComparison.OrdinalIgnoreCase)) break;
                    if (string.IsNullOrWhiteSpace(raw)) continue;

                    if (ParserFactory.TryParse(raw, out var weather))
                    {
                        Console.WriteLine(
                            $"Parsed: Location={weather.Location}, Temp={weather.Temperature}, Humidity={weather.Humidity}");
                        station.Publish(weather);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Could not parse input. Make sure it's valid JSON or XML matching the WeatherData schema.");
                    }
                }

                Console.WriteLine("Shutting down. Bye!");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fatal error: {ex.Message}");
                return 1;
            }
        }
    }
}
