using Common.Messaging.Abstractions.Events;
using Common.Persistence.Transactions.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Messaging.Transactional.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonTransactionalMessaging(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.TryAddScoped<TransactionalEventBuffer>();

            services.TryAddScoped<ITransactionalEventBuffer>(provider => provider.GetRequiredService<TransactionalEventBuffer>());

            services.TryAddScoped<ITransactionalEventCollector>(provider => provider.GetRequiredService<TransactionalEventBuffer>());

            services.TryAddEnumerable(ServiceDescriptor.Scoped<ITransactionParticipant, PublishEventsAfterCommitParticipant>());

            return services;
        }
    }
}
