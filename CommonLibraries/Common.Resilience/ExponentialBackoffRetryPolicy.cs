using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Resilience
{
    public sealed class ExponentialBackoffRetryPolicy(RetryOptions options, Func<Exception, bool> shouldRetry) : RetryPolicy(options)
    {
        private readonly RetryOptions _options = options;
        private readonly Func<Exception, bool> _shouldRetry = shouldRetry;

        protected override bool ShouldRetry(Exception ex) => _shouldRetry(ex);

        protected override TimeSpan CalculateDelay(int attempt)
        {
            var exponentialDelay = TimeSpan.FromMilliseconds(_options.BaseDelay.TotalMilliseconds * Math.Pow(2, attempt - 1));

            if(exponentialDelay > _options.MaxDelay)
                exponentialDelay = _options.MaxDelay;

            if(!_options.UseJitter)
                return exponentialDelay;

            var jitter = Random.Shared.Next(0, 100);

            return exponentialDelay + TimeSpan.FromMilliseconds(jitter);
        }
    }
}
