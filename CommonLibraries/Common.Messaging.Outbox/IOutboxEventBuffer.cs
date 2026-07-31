using Common.Messaging.Outbox.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox
{
    public interface IOutboxEventBuffer
    {
        IReadOnlyCollection<IOutboxEvent> Drain();

        void Clear();
    }
}
