using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public class ProblemDetails
    {
        public string? Type { get; init; }
        public string? Title { get; init; }
        public int? Status { get; init; }
        public string? Detail { get; init; }
        public string? Instance { get; init; }

        public string? ErrorCode { get; init; }

        public string? TraceId { get; init; }
        public string? CorrelationId { get; init; }

        public DateTimeOffset TimeStamp { get; set; }

        public Dictionary<string, object?> Extensions { get; init; } = [];
    }
}
