using Common.Persistence.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Common.Persistence.EntityFramework
{
    public sealed class EfTransaction(IDbContextTransaction transaction) : ITransaction
    {
        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return transaction.CommitAsync(cancellationToken);
        }

        public ValueTask DisposeAsync()
        {
            return transaction.DisposeAsync();
        }

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            return transaction.RollbackAsync(cancellationToken);
        }
    }
}
