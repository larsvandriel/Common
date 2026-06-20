using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Abstractions
{
    public interface ITransactionManager
    {
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
