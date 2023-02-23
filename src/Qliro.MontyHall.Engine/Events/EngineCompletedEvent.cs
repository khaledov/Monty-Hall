using MediatR;
using Qliro.MontyHall.Engine.Models;

namespace Qliro.MontyHall.Engine.Events
{
    public class EngineCompletedEvent : INotification
    {
        public Statistics Statistics { get; set; }
        public EngineCompletedEvent(Statistics statistics) { Statistics = statistics; }
    }
}
