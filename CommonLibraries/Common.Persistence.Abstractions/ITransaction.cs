using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Abstractions
{
    public interface ITransaction : IAsyncDisposable
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}
