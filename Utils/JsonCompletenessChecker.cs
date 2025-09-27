using WeatherBots.Utils;

internal sealed class JsonCompletenessChecker : ICompletenessChecker
{
    private int _brace, _bracket;

    public bool IsComplete => _brace <= 0 && _bracket <= 0;

    public void Consume(string line)
    {
        foreach (var ch in line)
        {
            if (ch == '{') _brace++;
            else if (ch == '}') _brace--;
            else if (ch == '[') _bracket++;
            else if (ch == ']') _bracket--;
        }
    }

    public void Reset() => (_brace, _bracket) = (0, 0);
}
