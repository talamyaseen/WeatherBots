using System.Text.RegularExpressions;
using WeatherBots.Utils;

internal sealed class XmlCompletenessChecker : ICompletenessChecker
{
    private string? _root;
    private bool _foundClosing;

    public bool IsComplete => _foundClosing;

    public void Consume(string line)
    {
        if (_root == null)
        {
            var match = Regex.Match(line, @"^<\s*([\w:\-]+)");
            _root = match.Success ? match.Groups[1].Value : "Document";
        }

        if (_root != null && line.Contains($"</{_root}>"))
            _foundClosing = true;
    }

    public void Reset() => (_root, _foundClosing) = (null, false);
}
