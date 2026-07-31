using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox
{
    public interface IOutboxProcessor
    {
        Task<int> ProcessAsync(CancellationToken cancellationToken = default);
    }
}
