using Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Resilience
{
    public interface IResilientTransactionalExecutor
    {
        Task<Result> ExecuteAsync<TService>(
            Func<TService, CancellationToken, Task<Result>> action,
            CancellationToken cancellationToken = default)
            where TService : notnull;

        Task<Result<T>> ExecuteAsync<TService, T>(
            Func<TService, CancellationToken, Task<Result<T>>> action,
            CancellationToken cancellationToken = default)
            where TService : notnull;
    }
}
