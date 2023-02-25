using MediatR;
using MontyHall.Engine.Events;
using MontyHall.Engine.Formatters;

namespace MontyHall.Engine.Handlers
{
    public class StatisticsWriter : INotificationHandler<EngineCompletedEvent>
    {
        static bool isLoaded = false;
        public Task Handle(EngineCompletedEvent notification, CancellationToken cancellationToken)
        {
            if (isLoaded) return Task.CompletedTask;
            isLoaded = true;
            ConsoleFormatter.WriteResult(notification.Statistics);
            return Task.CompletedTask;
        }
    }
}
