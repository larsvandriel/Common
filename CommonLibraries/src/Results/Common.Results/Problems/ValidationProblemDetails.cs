namespace Common.Results.Problems
{
    public sealed class ValidationProblemDetails : ProblemDetails
    {
        public Dictionary<string, string[]> Errors { get; init; } = [];
    }
}
