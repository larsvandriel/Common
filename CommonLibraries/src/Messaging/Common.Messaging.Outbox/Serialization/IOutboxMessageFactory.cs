using Common.Messaging.Outbox.Contracts;

namespace Common.Messaging.Outbox.Serialization
{
    public interface IOutboxMessageFactory
    {
        OutboxMessage Create(IOutboxEvent @event);
    }
}
