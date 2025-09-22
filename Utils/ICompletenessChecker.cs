namespace WeatherBots.Utils
{
    public interface ICompletenessChecker
    {
        void Consume(string line);
        bool IsComplete { get; }
        void Reset();
    }
}
