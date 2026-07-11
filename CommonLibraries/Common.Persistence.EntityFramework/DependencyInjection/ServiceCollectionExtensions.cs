using Common.Persistence.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.EntityFramework.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonPersistenceEntityFramework<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            ArgumentNullException.ThrowIfNull(services);
            
            services.TryAddScoped<IUnitOfWork, EfUnitOfWork<TDbContext>>();
            services.TryAddScoped<ITransactionManager, EfTransactionManager<TDbContext>>();

            return services;
        }
    }
}
