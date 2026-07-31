using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox.Abstractions
{
    public interface IOutboxTransport
    {
        Task PublishAsync(OutboxMessage message, CancellationToken cancellationToken = default);
    }
}
