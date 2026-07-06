using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public class ProblemDetails
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int? Status { get; set; }
        public string? Detail { get; set; }
        public string? Instance { get; set; }

        public string? ErrorCode { get; set; }
        public Exception? Exception { get; set; }

        public string? TraceId { get; set; }
        public string? CorrelationId { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public Dictionary<string, object?> Extensions { get; } = [];
    }
}
