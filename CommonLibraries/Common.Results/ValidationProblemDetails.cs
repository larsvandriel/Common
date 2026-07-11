namespace Common.Results
{
    public sealed class ValidationProblemDetails : ProblemDetails
    {
        public Dictionary<string, string[]> Errors { get; init; } = [];
    }
}
