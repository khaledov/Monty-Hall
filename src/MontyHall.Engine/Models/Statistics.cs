

namespace MontyHall.Engine.Models
{
    public class Statistics
    {
        public long GamesCount { get; internal set; }
        public long WinsCount { get; internal set; }
        public string? Strategy { get; internal set; }
        public double Accuracy => (WinsCount / (double)GamesCount);

        public void IncrementWins() => WinsCount++;
    }
}
