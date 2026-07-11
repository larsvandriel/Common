namespace Common.Resilience
{
    public sealed class ExponentialBackoffRetryPolicy(RetryOptions options, Func<Exception, bool> shouldRetry) : RetryPolicy(options)
    {
        private readonly RetryOptions _options = options;
        private readonly Func<Exception, bool> _shouldRetry = shouldRetry;

        protected override bool ShouldRetry(Exception ex) => _shouldRetry(ex);

        protected override TimeSpan CalculateDelay(int attempt)
        {
            if (_options.BaseDelay == TimeSpan.Zero)
                return TimeSpan.Zero;

            var exponentialMultiplier = Math.Pow(2, attempt - 1);

            var delayMilliseconds = Math.Min(
                _options.BaseDelay.TotalMilliseconds * exponentialMultiplier,
                _options.MaxDelay.TotalMilliseconds);

            if (_options.UseJitter)
                delayMilliseconds *= Random.Shared.NextDouble() * 0.5 + 0.75;

            delayMilliseconds = Math.Min(delayMilliseconds, _options.MaxDelay.TotalMilliseconds);

            return TimeSpan.FromMilliseconds(delayMilliseconds);
        }
    }
}
