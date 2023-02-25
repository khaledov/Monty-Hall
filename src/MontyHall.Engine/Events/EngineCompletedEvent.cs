using MediatR;
using MontyHall.Engine.Models;

namespace MontyHall.Engine.Events
{
    public class EngineCompletedEvent : INotification
    {
        public Statistics Statistics { get; set; }
        public EngineCompletedEvent(Statistics statistics) { Statistics = statistics; }
    }
}
