using Common.Results.AspNetCore.Enrichment;
using Common.Results.AspNetCore.Mapping;
using Common.Results.Enrichment;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Common.Results.AspNetCore.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonResultsAspNetCore(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddHttpContextAccessor();

            services.TryAddScoped<IHttpResultMapper, HttpResultMapper>();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IProblemDetailsEnricher, HttpProblemDetailsEnricher>());

            return services;
        }
    }
}
