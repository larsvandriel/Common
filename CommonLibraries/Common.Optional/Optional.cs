using System.Diagnostics.CodeAnalysis;

namespace Common.Optional
{
    public readonly struct Optional<T>(T? value)
    {
        public bool IsSpecified { get; } = true;
        public T? Value { get; } = value;

        public readonly T ValueOrThrow()
        {
            if (!IsSpecified)
                throw new InvalidOperationException("The optional value has not been set.");

            return Value!;
        }

        public readonly T? GetValueOrDefault(T? defaultValue = default)
        {
            return IsSpecified ? Value : defaultValue;
        }

        public readonly bool TryGetValue(out T? value)
        {
            value = Value;
            return IsSpecified;
        }

        public static implicit operator Optional<T>(T? value) => new(value);
    }
}
