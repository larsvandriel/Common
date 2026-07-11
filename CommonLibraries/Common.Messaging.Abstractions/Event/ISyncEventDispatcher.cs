namespace Common.Messaging.Abstractions.Event
{
    public interface ISyncEventDispatcher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
