using MediatR;
using MontyHall.Engine.Events;
using MontyHall.Engine.Formatters;

namespace MontyHall.Engine.Handlers
{
    internal class PlayStickHandler : INotificationHandler<RoundDone>
    {
        public Task Handle(RoundDone notification, CancellationToken cancellationToken)
        {
            Thread.Sleep(10);
            ConsoleFormatter.WriteProgress((int)notification.Progress,true);
            return Task.CompletedTask;
        }
    }
}
