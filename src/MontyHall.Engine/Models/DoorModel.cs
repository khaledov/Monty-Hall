

namespace MontyHall.Engine.Models
{
    public class DoorModel
    {
        public string? Prize { get; internal set; }

        public DoorState State { get; internal set; }

        public DoorModel(string prize, DoorState state)
        {
            Prize = prize;
            State = state;
        }
    }
}
