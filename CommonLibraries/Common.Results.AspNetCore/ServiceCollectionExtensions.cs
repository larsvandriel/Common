using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProblemDetailsEnricher<TEnricher>(this IServiceCollection services)
            where TEnricher : class, IProblemDetailEnricher
        {
            services.AddSingleton<IProblemDetailEnricher, TEnricher>();
            return services;
        }
    }
}
