using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox.Abstractions
{
    public interface IOutboxWriter
    {
        void Add(OutboxMessage message);

        void AddRange(IReadOnlyCollection<OutboxMessage> messages);
    }
}
