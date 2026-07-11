using Common.Persistence.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.EntityFramework
{
    public sealed class EfTransactionManager<TDbContext>(TDbContext dbContext) : ITransactionManager where TDbContext : DbContext
    {
        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }
    }
}
