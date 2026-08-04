namespace Common.Messaging.Abstractions.Events
{
    public interface ISyncEventDispatcher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
