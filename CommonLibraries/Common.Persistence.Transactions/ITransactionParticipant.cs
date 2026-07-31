using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Transactions
{
    public interface ITransactionParticipant
    {
        Task PrepareAsync(CancellationToken cancellationToken = default);

        Task CommittedAsync(CancellationToken cancellationToken = default);

        Task AbortedAsync(CancellationToken cancellationToken = default);
    }
}
