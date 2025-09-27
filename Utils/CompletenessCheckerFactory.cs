using WeatherBots.Utils;

namespace WeatherBots.Utils
{
    public static class CompletenessCheckerFactory
    {
        public static ICompletenessChecker CreateChecker(string firstLine)
        {
            var trimmed = firstLine.TrimStart();

            if (trimmed.StartsWith("{") || trimmed.StartsWith("["))
                return new JsonCompletenessChecker();

            if (trimmed.StartsWith("<"))
                return new XmlCompletenessChecker();

            throw new NotSupportedException("Unsupported document format for completeness checking.");
        }
    }
}
