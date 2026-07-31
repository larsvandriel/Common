using Common.Messaging.Outbox.Abstractions;
using Common.Persistence.Transactions;

namespace Common.Messaging.Outbox
{
    public sealed class OutboxTransactionParticipant(IOutboxEventBuffer eventBuffer, IOutboxMessageFactory messageFactory, IOutboxWriter writer) : ITransactionParticipant
    {
        public Task PrepareAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var events = eventBuffer.Drain();

            if(events.Count == 0)
                return Task.CompletedTask;

            var messages = events.Select(messageFactory.Create).ToArray();

            writer.AddRange(messages);

            return Task.CompletedTask;
        }

        public Task CommittedAsync(CancellationToken cancellationToken = default)
        {
            eventBuffer.Clear();
            return Task.CompletedTask;
        }

        public Task AbortedAsync(CancellationToken cancellationToken = default)
        {
            eventBuffer.Clear();
            return Task.CompletedTask;
        }
    }
}
