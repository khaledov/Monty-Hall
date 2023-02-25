using MediatR;

namespace MontyHall.Engine.Events
{
    public class RoundDone : INotification
    {
        public long Progress { get; set; }
        public RoundDone(long progress) {
        Progress= progress;
        }
    }
}
