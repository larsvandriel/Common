namespace Common.Messaging.Outbox.Abstractions
{
    public interface IOutboxEventCollector
    {
        void Add(IOutboxEvent @event);
    }
}
