using MediatR;
using Qliro.MontyHall.Engine.Events;
using Qliro.MontyHall.Engine.Formatters;

namespace Qliro.MontyHall.Engine.Handlers
{
    public class StatisticsWriter : INotificationHandler<EngineCompletedEvent>
    {
        public Task Handle(EngineCompletedEvent notification, CancellationToken cancellationToken)
        {
            ConsoleFormatter.WriteResult(notification.Statistics);
            Environment.Exit(-1);
            return Task.CompletedTask;
        }
    }
}
