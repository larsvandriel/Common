using Common.Messaging.Abstractions.Event;
using Common.Persistence.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Transactional
{
    public sealed class PublishEventsAfterCommitParticipant(ITransactionalEventBuffer buffer, IEventDispatcher dispatcher) : ITransactionParticipant
    {
        public Task AbortedAsync(CancellationToken cancellationToken = default)
        {
            buffer.Clear();
            return Task.CompletedTask;
        }

        public async Task CommittedAsync(CancellationToken cancellationToken = default)
        {
            foreach (var @event in buffer.TakeAll())
            {
                await dispatcher.PublishAsync(@event, cancellationToken);
            }
        }

        public Task PrepareAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
