namespace Common.Results.Problems
{
    public class ProblemDetails
    {
        public string Type { get; init; } = "about:blank";
        public string Title { get; init; } = "An error occurred.";
        public int Status { get; init; }
        public string? Detail { get; init; }
        public string? Instance { get; set; }

        public string? ErrorCode { get; init; }

        public string? TraceId { get; set; }
        public string? CorrelationId { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public Dictionary<string, object?> Extensions { get; } = [];
    }
}
