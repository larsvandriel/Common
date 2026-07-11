using Common.Persistence.Concurrency;
using Common.Persistence.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.EntityFramework
{
    public sealed class EfUnitOfWork<TDbContext>(TDbContext dbContext) : IUnitOfWork where TDbContext : DbContext
    {
        private readonly DbContext _dbContext = dbContext;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(DbUpdateConcurrencyException exception)
            {
                throw new ConcurrencyConflictException("The persisted data was changed by another operation.", exception);
            }
        }
    }
}
