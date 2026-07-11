using Common.Persistence.Resilience.Execution;
using Common.Persistence.Resilience.Retry;
using Common.Persistence.Transactions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Common.Persistence.Resilience.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonPersistenceResilience(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);
            
            services.AddOptions<TransactionalRetryOptions>().Bind(configuration.GetSection(TransactionalRetryOptions.SectionName)).ValidateOnStart();

            return services.AddCommonPersistenceResilienceCore();
        }

        public static IServiceCollection AddCommonPersistenceResilience(this IServiceCollection services, Action<TransactionalRetryOptions> configure)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configure);

            services.AddOptions<TransactionalRetryOptions>().Configure(configure).ValidateOnStart();

            return services.AddCommonPersistenceResilienceCore();
        }

        private static IServiceCollection AddCommonPersistenceResilienceCore(this IServiceCollection services)
        {
            services.AddCommonPersistenceTransactions();

            services.TryAddSingleton<IResilientTransactionalExecutor, ResilientTransactionalExecutor>();

            services.TryAddSingleton<ITransactionalRetryPolicy, TransactionalRetryPolicy>();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<ITransactionalRetryExceptionClassifier, ConcurrencyConflictRetryExceptionClassifier>());

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<TransactionalRetryOptions>, TransactionalRetryOptionsValidator>());

            return services;
        }
    }
}
