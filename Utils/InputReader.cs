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

            // ask factory to give the correct checker
            var checker = CompletenessCheckerFactory.CreateChecker(firstLine);

            string? next = firstLine;
            while (!checker.IsComplete && next is not null)
            {
                sb.AppendLine(next);
                checker.Consume(next);
                next = Console.ReadLine();
            }

            var documentText = sb.ToString().Trim();
            return string.IsNullOrWhiteSpace(documentText) ? null : documentText;
        }
    }
}
