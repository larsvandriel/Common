using Common.Resilience;
using Common.Results;
using System.Runtime.InteropServices.Marshalling;

namespace Common.Persistence.Transactions
{
    public class TransactionalAttemptExecutor(ITransactionManager transactionManager, IUnitOfWork unitOfwork) : ITransactionalAttemptExecutor
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
            await using var transaction = await transactionManager.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await action(cancellationToken);

                if (shouldRollback(result))
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return result;
                }

                await unitOfwork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}
