

namespace Qliro.MontyHall.Engine.Models
{
    public class Statistics
    {
        public long GamesCount { get; internal set; }
        public long WinsCount { get; internal set; }
        public string? Strategy { get; internal set; }
        public string Accuracy => (WinsCount / (decimal)GamesCount).ToString("0.00");

        public void IncrementWins() => WinsCount++;
    }
}
