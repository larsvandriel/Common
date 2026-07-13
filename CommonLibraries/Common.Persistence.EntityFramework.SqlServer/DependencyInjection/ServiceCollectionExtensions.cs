using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Persistence.EntityFramework.SqlServer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSqlServerConflictDetector(this IServiceCollection services)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IDbUpdateConcurrencyConflictDetector, SqlServerDuplicateKeyConflictDetector>());

            return services;
        }
    }
}
