using Common.Results.Enrichment;
using Common.Results.Problems;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Common.Results.AspNetCore.Enrichment
{
    public sealed class HttpProblemDetailsEnricher (IHttpContextAccessor httpContextAccessor) : IProblemDetailsEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public ProblemDetails Enrich(ProblemDetails problem)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            problem.Instance ??= httpContext?.Request.Path.Value;
            problem.TraceId ??= Activity.Current?.TraceId.ToString();
            problem.CorrelationId ??= httpContext?.TraceIdentifier;
            problem.Timestamp = DateTimeOffset.UtcNow;

            return problem;
        }
    }
}
