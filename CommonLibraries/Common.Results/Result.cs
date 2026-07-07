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

    public sealed class Result<T> : Result
    {
        private readonly T? _value;

        public T Value => IsSuccess ? _value! : throw new InvalidOperationException("Cannot access Value when result is failed.");


        private Result(bool isSuccess, T? value, ProblemDetails? problem) : base(isSuccess, problem)
        {
            _value = value;
        }

        public static Result<T> Success(T value) => new(true, value, null);

        public static new Result<T> Failure(ProblemDetails problem) => new(false, default, problem);

        public static implicit operator Result<T>(T value) => Success(value);
    }
}
