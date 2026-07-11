using Common.Results;
using Microsoft.Extensions.Logging;

namespace Common.Persistence.Transactions.Execution
{
    public sealed class TransactionalAttemptExecutor(
        ITransactionManager transactionManager,
        IUnitOfWork unitOfwork,
        ILogger<TransactionalAttemptExecutor> logger)
        : ITransactionalAttemptExecutor
    {
        public Task<Result> ExecuteAsync(Func<CancellationToken, Task<Result>> action, CancellationToken cancellationToken = default)
        {
            return ExecuteInternalAsync(action, result => result.IsFailure, cancellationToken);
        }

        public Task<Result<T>> ExecuteAsync<T>(Func<CancellationToken, Task<Result<T>>> action, CancellationToken cancellationToken = default)
        {
            return ExecuteInternalAsync(action, result => result.IsFailure, cancellationToken);
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
                    await RollbackAsync(transaction);
                    return result;
                }

                await unitOfwork.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return result;
            }
            catch(Exception originalException)
            {
                await TryRollbackAsync(transaction, originalException);
                throw;
            }
        }

        private static Task RollbackAsync(ITransaction transaction)
        {
            return transaction.RollbackAsync(CancellationToken.None);
        }

        private async Task TryRollbackAsync(ITransaction transaction, Exception originalException)
        {
            try
            {
                await RollbackAsync(transaction);
            }
            catch(Exception rollbackException)
            {
                logger.LogError(rollbackException,
                    """
                    Rolling back the transaction failed after an earlier exception.
                    The original exception will be rethrown.
                    Original exception: {OrignalExceptionType}: {OriginalExceptionMessage}
                    """,
                    originalException.GetType().FullName,
                    originalException.Message);
            }
        }
    }
}
