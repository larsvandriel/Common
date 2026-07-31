using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox.Abstractions
{
    public interface IOutboxEventTypeResolver
    {
        string Resolve(Type eventType);
    }
}
