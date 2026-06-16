using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public sealed class Result<T>: Result
    {
        public T? Value { get; }

        private Result(bool isSuccess, T? value, string? errorMessage) : base(isSuccess, errorMessage)
        {
            Value = value;
        }

        public static Result<T> Success(T value) => new(true, value, null);

        public static new Result<T> Failure(string errorMessage) => new(false, default, errorMessage);

        public static implicit operator Result<T>(T value) => Success(value);
    }
}
