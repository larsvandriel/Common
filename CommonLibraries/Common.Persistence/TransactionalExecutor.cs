using Common.Persistence.Abstractions;
using Common.Resilience;
using Common.Results;
using System.Runtime.InteropServices.Marshalling;

namespace Common.Persistence
{
    public class TransactionalExecutor(ITransactionManager transactionManager, IUnitOfWork unitOfwork, IRetryPolicy retryPolicy) : ITransactionalExecutor
    {
        public Task<Result> ExecuteAsync(Func<CancellationToken, Task<Result>> action, CancellationToken cancellationToken = default)
        {
            return ExecuteInternalAsync(action, result => !result.IsSuccess, cancellationToken);
        }

        public Task<Result<T>> ExecuteAsync<T>(Func<CancellationToken, Task<Result<T>>> action, CancellationToken cancellationToken = default)
        {
            return ExecuteInternalAsync(action, result => !result.IsSuccess, cancellationToken);
        }

        private async Task<TResult> ExecuteInternalAsync<TResult>(
            Func<CancellationToken, Task<TResult>> action,
            Func<TResult, bool> shouldRollback,
            CancellationToken cancellationToken)
        {
            return await retryPolicy.ExecuteAsync(async ct =>
            {
                await using var transaction = await transactionManager.BeginTransactionAsync(ct);

                try
                {
                    var result = await action(ct);

                    if (shouldRollback(result))
                    {
                        await transaction.RollbackAsync(ct);
                        return result;
                    }

                    await unitOfwork.SaveChangesAsync(ct);
                    await transaction.CommitAsync(ct);

                    return result;
                }
                catch
                {
                    await transaction.RollbackAsync(ct);
                    throw;
                }
            }, cancellationToken);
        }
    }
}
