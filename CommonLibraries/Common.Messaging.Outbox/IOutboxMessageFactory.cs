using Common.Messaging.Outbox.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox
{
    public interface IOutboxMessageFactory
    {
        OutboxMessage Create(IOutboxEvent @event);
    }
}
