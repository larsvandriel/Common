using Microsoft.AspNetCore.Http;
using AspNetResults = Microsoft.AspNetCore.Http.Results;

namespace Common.Results.AspNetCore.Mapping
{
    public sealed class HttpResultMapper(IProblemDetailsEnricher problemDetailsEnricher) : IHttpResultMapper
    {
        public IResult Map(Result result)
        {
            if (result.IsSuccess)
                return AspNetResults.NoContent();

            return MapProblem(result.Problem!);
        }

        public IResult Map<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return AspNetResults.Ok(result.Value);

            return MapProblem(result.Problem!);
        }

        private IResult MapProblem(ProblemDetails problem)
        {
            var enrichedProblem = problemDetailsEnricher.Enrich(problem);

            var extensions = new Dictionary<string, object?>(enrichedProblem.Extensions);

            if(enrichedProblem.ErrorCode is not null)
                extensions["errorCode"] = enrichedProblem.ErrorCode;

            if (enrichedProblem.TraceId is not null)
                extensions["traceId"] = enrichedProblem.TraceId;

            if (enrichedProblem.CorrelationId is not null)
                extensions["correlationId"] = enrichedProblem.CorrelationId;

            extensions["timestamp"] = enrichedProblem.TimeStamp;

            return AspNetResults.Problem(
                type: enrichedProblem.Type,
                title: enrichedProblem.Title,
                statusCode: enrichedProblem.Status,
                detail: enrichedProblem.Detail,
                instance: enrichedProblem.Instance,
                extensions: extensions);
        }
    }
}
