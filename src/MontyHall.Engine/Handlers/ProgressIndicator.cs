using MediatR;
using MontyHall.Engine.Events;
using MontyHall.Engine.Formatters;

namespace MontyHall.Engine.Handlers
{
    public class ProgressIndicator : INotificationHandler<ProgressIncrementedEvent>
    {
        public Task Handle(ProgressIncrementedEvent notification, CancellationToken cancellationToken)
        {
           
            ConsoleFormatter.WriteProgressBar(notification.Percent, true);
            return Task.CompletedTask;
        }
    }
}
