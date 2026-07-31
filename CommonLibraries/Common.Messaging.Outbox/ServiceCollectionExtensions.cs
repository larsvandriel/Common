using Common.Messaging.Outbox.Abstractions;
using Common.Persistence.Transactions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Common.Messaging.Outbox
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonMessagingOutbox(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            services.AddOptions<OutboxOptions>().Bind(configuration.GetSection(OutboxOptions.SectionName)).ValidateOnStart();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<OutboxOptions>, OutboxOptionsValidator>());

            services.TryAddSingleton(TimeProvider.System);
            services.TryAddSingleton(new JsonSerializerOptions(JsonSerializerDefaults.Web));

            services.TryAddSingleton<IOutboxEventTypeResolver, AttributeOutboxEventTypeResolver>();

            services.TryAddScoped<OutboxEventBuffer>();

            services.TryAddScoped<IOutboxEventCollector>(provider => provider.GetRequiredService<OutboxEventBuffer>());

            services.TryAddScoped<IOutboxEventBuffer>(provider => provider.GetRequiredService<OutboxEventBuffer>());

            services.TryAddScoped<IOutboxMessageFactory, JsonOutboxMessageFactory>();

            services.TryAddEnumerable(ServiceDescriptor.Scoped<ITransactionParticipant, OutboxTransactionParticipant>());

            services.TryAddScoped<IOutboxProcessor, OutboxProcessor>();

            return services;
        }
    }
}
