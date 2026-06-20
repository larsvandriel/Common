using Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Abstractions
{
    public interface ITransactionalExecutor
    {
        Task<Result> ExecuteAsync(Func<CancellationToken, Task<Result>> action, CancellationToken cancellationToken = default);

        Task<Result<T>> ExecuteAsync<T>(Func<CancellationToken, Task<Result<T>>> action, CancellationToken cancellationToken = default);
    }
}
