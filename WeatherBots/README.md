WeatherBots is a C# console app that monitors real-time weather data from multiple stations in JSON or XML formats and activates different “weather bots” based on configured thresholds.
Bots are controlled via
{
RainBot: {enabled: true, humidityThreshold: 70, message: It looks like it's about to pour down!},
SunBot: {enabled: true, temperatureThreshold: 30, message: Wow, it's a scorcher out there!},
SnowBot: {enabled: false, temperatureThreshold: 0, message: Brrr, it's getting chilly!}
}

1. Run the app.
2. Enter weather data in JSON or XML:
   {Location: City, Temperature: 32, Humidity: 40}
3. Bots activate according to configuration and display their messages.
   **Notes:**

- Follows SOLID principles, allowing easy addition of new bots or data formats.
- Uses Observer and Strategy design patterns.
