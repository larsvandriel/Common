namespace Common.Resilience
{
    public class RetryOptions
    {
        public static RetryOptions Default => new();

        public int MaxAttempts { get; init; } = 3;

        public TimeSpan BaseDelay { get; init; } = TimeSpan.FromMicroseconds(250);

        public TimeSpan MaxDelay { get; init; } = TimeSpan.FromSeconds(10);

        public bool UseJitter { get; init; } = true;
    }
}
