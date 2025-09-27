using System;
using System.Text;
using WeatherBots.Utils;

namespace WeatherBots.Utils
{
    public static class InputReader
    {
        public static string? ReadDocument()
        {
            var firstLine = Console.ReadLine();
            if (firstLine == null) return null;

            firstLine = firstLine.Trim();
            if (string.IsNullOrWhiteSpace(firstLine)) return null;

            var sb = new StringBuilder();
            var checker = CompletenessCheckerFactory.CreateChecker(firstLine);

            string? line = firstLine;
            while (line != null)
            {
                sb.AppendLine(line);
                checker.Consume(line);

                if (checker.IsComplete)
                    break;

                line = Console.ReadLine();
            }

            var documentText = sb.ToString().Trim();
            return string.IsNullOrWhiteSpace(documentText) ? null : documentText;
        }
    }
}
