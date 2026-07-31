using Common.Messaging.Outbox.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonMessagingOutboxEntityFramework<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            ArgumentNullException.ThrowIfNull(services);

            services.TryAddScoped<IOutboxWriter, EfOutboxWriter<TDbContext>>();

            services.TryAddScoped<IOutboxStore, EfOutboxStore<TDbContext>>();

            return services;
        }
    }
}
