using System;
using System.Text;
using WeatherBots.Utils;

namespace WeatherBots.Utils
{
    internal static class InputReader
    {
        public static string? ReadDocument()
        {
            var firstLine = Console.ReadLine();
            if (firstLine == null) return null;

            firstLine = firstLine.TrimEnd();
            if (string.IsNullOrWhiteSpace(firstLine)) return firstLine;

            var sb = new StringBuilder();
            sb.AppendLine(firstLine);

            // ask factory to give the correct checker
            var checker = CompletenessCheckerFactory.CreateChecker(firstLine);
            checker.Consume(firstLine);

            while (!checker.IsComplete)
            {
                var next = Console.ReadLine();
                if (next == null) break;
                sb.AppendLine(next);
                checker.Consume(next);
            }

            var documentText = sb.ToString().Trim();
            return string.IsNullOrWhiteSpace(documentText) ? null : documentText;
        }
    }
}
