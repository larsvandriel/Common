using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails Create(
            string type,
            string title,
            int status,
            string detail,
            string? instance = null,
            string? errorCode = null,
            string? traceId = null,
            string? correlationId = null)
        {
            return new ProblemDetails
            {
                Type = type,
                Title = title,
                Status = status,
                Detail = detail,
                Instance = instance,
                ErrorCode = errorCode,
                TraceId = traceId,
                CorrelationId = correlationId,
                TimeStamp = DateTimeOffset.UtcNow
            };
        }

        public static ValidationProblemDetails CreateValidation(
            string type,
            string detail,
            Dictionary<string, string[]> errors,
            string? instance = null,
            string? errorCode = null,
            string? traceId = null,
            string? correlationId = null)
        {
            return new ValidationProblemDetails
            {
                Type = type,
                Title = "Validation error",
                Status = 400,
                Detail = detail,
                Instance = instance,
                ErrorCode = errorCode,
                TraceId = traceId,
                CorrelationId = correlationId,
                TimeStamp = DateTimeOffset.UtcNow,
                Errors = errors
            };
        }
    }
}
