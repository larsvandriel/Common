using Common.Results.AspNetCore.Mapping;
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

            services.TryAddSingleton<IProblemDetailsEnricher, ProblemDetailsEnricher>();

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IProblemDetailEnricher, HttpProblemDetailsEnricher>());

            return services;
        }
    }
}
