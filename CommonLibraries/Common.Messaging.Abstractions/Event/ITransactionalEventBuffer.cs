using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Event
{
    public interface ITransactionalEventBuffer
    {
        IReadOnlyList<IEvent> TakeAll();
        void Clear();
    }
}
