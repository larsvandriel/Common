namespace Common.Messaging.Abstractions.Event
{
    public interface ISyncEventHandler<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}
