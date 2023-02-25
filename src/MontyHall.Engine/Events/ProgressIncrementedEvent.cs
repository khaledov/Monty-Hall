using MediatR;

namespace MontyHall.Engine.Events
{
    public class ProgressIncrementedEvent : INotification
    {
        public long Percent { get; set; }
        public ProgressIncrementedEvent(long percent)
        {
            Percent = percent;
        }
    }
}
