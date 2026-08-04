using Common.Messaging.Outbox.Contracts;

namespace Common.Messaging.Outbox.Transactions
{
    public interface IOutboxEventBuffer
    {
        IReadOnlyCollection<IOutboxEvent> Drain();

        void Clear();
    }
}
