namespace Common.Messaging.Outbox.Contracts
{
    public interface IOutboxEventCollector
    {
        void Add(IOutboxEvent @event);
    }
}
