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
            Exception? exception = null,
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
                Exception = exception,
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
            Exception? exception = null,
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
                Exception = exception,
                TraceId = traceId,
                CorrelationId = correlationId,
                TimeStamp = DateTimeOffset.UtcNow,
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

        public static ProblemDetails Unexpected(Exception exception, string detail = "An unexpected error occurred.", string? instance = null)
        {
            return Create("error:UnexpectedError", "Unexpected error", 500, detail, instance, exception: exception);
        }
    }
}
