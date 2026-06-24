using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public sealed class Result<T>: Result
    {
        private readonly T? _value;

        public T? Value
        {
            get
            {
                if (IsFailure)
                    throw new InvalidOperationException("Cannot acces Value when result is failed.");

                return _value;
            }
        }

        private Result(bool isSuccess, T? value, ProblemDetails? problem) : base(isSuccess, problem)
        {
            _value = value;
        }

        public static Result<T> Success(T value) => new(true, value, null);

        public static new Result<T> Failure(ProblemDetails problem) => new(false, default, problem);

        public static implicit operator Result<T>(T value) => Success(value);
    }
}
