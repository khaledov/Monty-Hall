using MediatR;

namespace Qliro.MontyHall.Engine.Events
{
    public class ProgressIncrementedEvent : INotification
    {
        public int Percent { get; set; }
        public ProgressIncrementedEvent(int percent)
        {
            Percent = percent;
        }
    }
}
