using Common.Messaging.Abstractions.Event;
using Common.Messaging.Abstractions.PubSub;
using Common.Messaging.Abstractions.Requests;
using Common.Messaging.Async.Events;
using Common.Messaging.Async.PubSub;
using Common.Messaging.Async.Requests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Common.Messaging.Async.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonMessagingAsync(this IServiceCollection services, params Assembly[] handlerAssemblies)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(handlerAssemblies);
            
            services.TryAddScoped<IRequestDispatcher, RequestDispatcher>();
            services.TryAddScoped<IEventDispatcher, EventDispatcher>();
            services.TryAddSingleton<IAsyncEventBus, AsyncEventBus>();

            services.AddHandlers(handlerAssemblies);

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services, Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(x => x.GetTypes()).Where(x => x is { IsAbstract: false, IsInterface: false });

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces().Where(i =>
                    i.IsGenericType &&
                    (
                        i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ||
                        i.GetGenericTypeDefinition() == typeof(IEventHandler<>)
                    ));

                foreach (var serviceType in interfaces)
                {
                    services.AddScoped(serviceType, type);
                }
            }

            return services;
        }
    }
}
