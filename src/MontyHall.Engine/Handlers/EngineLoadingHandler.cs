using MediatR;
using MontyHall.Engine.Events;
using MontyHall.Engine.Formatters;

namespace MontyHall.Engine.Handlers
{
    public class EngineLoadingHandler : INotificationHandler<EngineStartedEvent>
    {
        static bool isLoaded=false;
        public Task Handle(EngineStartedEvent notification, CancellationToken cancellationToken)
        {
            if(isLoaded) return Task.CompletedTask;
            isLoaded = true;
            ConsoleFormatter.WriteSpecial("Engine started, please wait ...");
            return Task.CompletedTask;
        }
    }
}
