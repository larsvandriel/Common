namespace Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public ProblemDetails? Problem { get; }

        protected Result(bool isSuccess, ProblemDetails? problem)
        {
            if (isSuccess && problem != null)
                throw new InvalidOperationException("A successful result cannot contain a problem.");

            if (!isSuccess && problem == null)
                throw new InvalidOperationException("A failed result must contain a problem.");

            IsSuccess = isSuccess;
            Problem = problem;
        }

        public static Result Success() => new(true, null);

        public static Result Failure(ProblemDetails problem) => new(false, problem);
    }
}
