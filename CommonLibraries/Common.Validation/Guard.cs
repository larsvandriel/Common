namespace Common.Validation
{
    public static class Guard
    {
        public static void NotNull<T>(T? value, string parameterName)
        {
            if (value is null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        public static void NotNullOrWhiteSpace(string? value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{parameterName}' cannot be null or whitespace.", parameterName);
            }
        }
    }
}
