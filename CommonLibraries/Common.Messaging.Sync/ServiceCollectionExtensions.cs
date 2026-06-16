using Common.Messaging.Abstractions.Event;
using Common.Messaging.Abstractions.Requests;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Messaging.Sync
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonMessagingSync(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<ISyncRequestDispatcher, SyncRequestDispatcher>();
            services.AddScoped<ISyncEventDispatcher, SyncEventDispatcher>();

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
                            i.GetGenericTypeDefinition() == typeof(ISyncRequestHandler<,>) ||
                            i.GetGenericTypeDefinition() == typeof(ISyncEventHandler<>)
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
