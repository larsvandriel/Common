namespace Common.Results.Problems
{
    public static class ProblemDetailsFactory
    {
        public static ProblemDetails Create(
            string type,
            string title,
            int status,
            string? detail,
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
                Timestamp = DateTimeOffset.UtcNow
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
                Timestamp = DateTimeOffset.UtcNow,
                Errors = errors
            };
        }

        public static ProblemDetails BusinessRule(string type, string detail, string? instance = null, string? errorCode = null)
        {
            return Create(type, "Business rule violation", 409, detail, instance, errorCode);
        }

        public static ProblemDetails NotFound(string type, string detail, string? instance = null, string? errorCode = null)
        {
            return Create(type, "Not found", 404, detail, instance, errorCode);
        }

        public static ProblemDetails Forbidden(string type, string detail, string? instance = null, string? errorCode = null)
        {
            return Create(type, "Forbidden", 403, detail, instance, errorCode);
        }

        public static ProblemDetails Conflict(string type, string detail, string? instance = null, string? errorCode = null)
        {
            return Create(type, "Conflict", 409, detail, instance, errorCode);
        }

        public static ProblemDetails Unexpected(string? instance = null, string? errorCode = "unexpected_error")
        {
            return Create(
                type: "urn:problem:unexpected-error",
                title: "Unexpected error",
                status: 500,
                detail: "An unexpected error occurred.",
                instance: instance,
                errorCode: errorCode);
        }
    }
}
