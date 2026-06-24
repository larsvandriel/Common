using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public sealed class ValidationProblemDetails : ProblemDetails
    {
        public Dictionary<string, string[]> Errors { get; init; } = [];
    }
}
