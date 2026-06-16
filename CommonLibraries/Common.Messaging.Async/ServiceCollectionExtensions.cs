using Common.Messaging.Abstractions.Event;
using Common.Messaging.Abstractions.Requests;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Messaging.Async
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonMessagingAsync(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IRequestDispatcher, RequestDispatcher>();
            services.AddScoped<IEventDispatcher, EventDispatcher>();

            services.RegisterHandlers(assemblies);

            return services;
        }

        private static IServiceCollection RegisterHandlers(this IServiceCollection services, Assembly[] assemblies)
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
