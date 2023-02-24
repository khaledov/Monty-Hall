using MediatR;
using Qliro.MontyHall.Engine.Events;
using Qliro.MontyHall.Engine.Formatters;

namespace Qliro.MontyHall.Engine.Handlers
{
    public class ProgressIndicator : INotificationHandler<ProgressIncrementedEvent>
    {
        public Task Handle(ProgressIncrementedEvent notification, CancellationToken cancellationToken)
        {
            // ConsoleFormatter.DrawProgress();
            //ConsoleFormatter.WriteProgress(notification.Percent, true);
            ConsoleFormatter.WriteProgressBar(notification.Percent, true);
            return Task.CompletedTask;
        }
    }
}
