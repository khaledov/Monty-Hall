

namespace Qliro.MontyHall.Engine.Models
{
    public class Statistics
    {
        public long GamesCount { get; internal set; }
        public long WinsCount { get; internal set; }
        public string? Strategy { get; internal set; }
        public decimal Accuracy => WinsCount / (decimal)GamesCount;

        public void IncrementWins() => WinsCount++;
    }
}
