using Common.Persistence.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.EntityFramework
{
    public sealed class EfTransactionManager(DbContext dbContext) : ITransactionManager
    {
        public async Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            return new EfTransaction(transaction);
        }
    }
}
