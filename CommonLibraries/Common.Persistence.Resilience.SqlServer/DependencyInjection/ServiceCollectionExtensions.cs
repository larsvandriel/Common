using Common.Persistence.Resilience.Retry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Persistence.Resilience.SqlServer.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonPersistenceResilienceSqlServer(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.TryAddEnumerable(ServiceDescriptor.Singleton<ITransactionalRetryExceptionClassifier, SqlServerTransactionalRetryExceptionClassifier>());

            return services;
        }
    }
}
