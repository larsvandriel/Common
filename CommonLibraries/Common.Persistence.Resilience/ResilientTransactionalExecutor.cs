using Common.Results;
using Common.Persistence.Transactions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection.Metadata.Ecma335;

namespace Common.Persistence.Resilience
{
    public sealed class ResilientTransactionalExecutor(
        IServiceScopeFactory scopeFactory,
        ITransactionalRetryPolicy retryPolicy) : IResilientTransactionalExecutor
    {
        public Task<Result> ExecuteAsync<TService>(
            Func<TService, CancellationToken, Task<Result>> action,
            CancellationToken cancellationToken = default)
            where TService : notnull
        {
            return retryPolicy.ExecuteAsync(
                async ct =>
                {
                    await using var scope = scopeFactory.CreateAsyncScope();

                    var service = scope.ServiceProvider.GetRequiredService<TService>();

                    var attemptExecutor = scope.ServiceProvider.GetRequiredService<ITransactionalAttemptExecutor>();

                    return await attemptExecutor.ExecuteAsync(attemptCt => action(service, attemptCt), ct);
                },
                cancellationToken);
        }

        public Task<Result<T>> ExecuteAsync<TService, T>(
            Func<TService, CancellationToken, Task<Result<T>>> action,
            CancellationToken cancellationToken = default)
            where TService : notnull
        {
            return retryPolicy.ExecuteAsync(
                async ct =>
                {
                    await using var scope = scopeFactory.CreateAsyncScope();

                    var service = scope.ServiceProvider.GetRequiredService<TService>();

                    var attemptExecutor = scope.ServiceProvider.GetRequiredService<ITransactionalAttemptExecutor>();

                    return await attemptExecutor.ExecuteAsync(attemptCt => action(service, attemptCt), ct);
                },
                cancellationToken);
        }
    }
}
