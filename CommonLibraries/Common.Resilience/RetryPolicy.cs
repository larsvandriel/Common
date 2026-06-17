using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Resilience
{
    public abstract class RetryPolicy : IRetryPolicy
    {
        private readonly RetryOptions _options;

        protected RetryPolicy(RetryOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            if(options.MaxAttempts < 1)
            {
                throw new ArgumentException("MaxAttempts must be at least 1.", nameof(options));
            }

            _options = options;
        }

        public async Task ExecuteAsync(Func<CancellationToken, Task> action, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(action);

            await ExecuteAsync(async ct => { await action(ct); return true; }, cancellationToken);
        }

        public async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(action);

            for (var attempt = 1; attempt <= _options.MaxAttempts; attempt++)
            {
                try
                {
                    return await action(cancellationToken);
                }
                catch (Exception ex) when (attempt < _options.MaxAttempts && ShouldRetry(ex))
                {
                    var delay = CalculateDelay(attempt);
                    await Task.Delay(delay, cancellationToken);
                }
            }

            throw new InvalidOperationException("Retry policy failed unexpectedly");
        }

        protected abstract bool ShouldRetry(Exception ex);

        protected abstract TimeSpan CalculateDelay(int attempt);
    }
}
