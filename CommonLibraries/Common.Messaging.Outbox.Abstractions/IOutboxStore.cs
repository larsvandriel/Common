using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox.Abstractions
{
    public interface IOutboxStore
    {
        Task<IReadOnlyCollection<OutboxMessage>> GetPendingAsync(int maximumCount, DateTimeOffset nowUtc, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
