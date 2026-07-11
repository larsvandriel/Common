using Common.Persistence.Transactions.Execution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Transactions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonPersistenceTransactions(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.TryAddScoped<ITransactionalAttemptExecutor, TransactionalAttemptExecutor>();

            return services;
        }
    }
}
