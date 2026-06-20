using Common.Persistence.Abstractions;
using Common.Results;

namespace Common.Persistence
{
    public class TransactionalExecutor(ITransactionManager transactionManager, IUnitOfWork unitOfwork) : ITransactionalExecutor
    {
        public async Task<Result> ExecuteAsync(Func<CancellationToken, Task<Result>> action, CancellationToken cancellationToken = default)
        {
            await using var transaction = await transactionManager.BeginTransactionAsync(cancellationToken);

            try
            {
                var result = await action(cancellationToken);

                if (!result.IsSuccess)
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
