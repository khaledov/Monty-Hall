using Qliro.MontyHall.Engine.Extensions;

namespace Qliro.MontyHall.Engine.Models
{
    public class GameModel
    {
        public const string GOAT = "GOAT";
        public const string GOLD = "GOLD";

        public bool Switch { get; set; }
        public long Tries { get; set; }

        public List<DoorModel> Doors { get; set; }

        public GameModel()
        {
            Doors = new List<DoorModel>
            {
                 new DoorModel (GOAT, DoorState.Default ),
                 new DoorModel (GOAT, DoorState.Default ),
                 new DoorModel (GOLD, DoorState.Default ),
            };
            Doors.Shuffle();
        }

    }
}
