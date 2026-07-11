using Common.Persistence.Concurrency;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Common.Persistence.Resilience
{
    public sealed class TransactionalRetryPolicy(
        IOptions<TransactionalRetryOptions> options,
        ILogger<TransactionalRetryPolicy> logger)
        : ITransactionalRetryPolicy
    {
        private readonly TransactionalRetryOptions _options = options.Value;
        private readonly ILogger<TransactionalRetryPolicy> _logger = logger;

        public async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(action);

            for (var attempt = 1; attempt <= _options.MaxAttempts; attempt++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    return await action(cancellationToken);
                }
                catch (Exception exception) when (ShouldRetry(exception, attempt, cancellationToken))
                {
                    var delay = CalculateDelay(attempt);

                    _logger.LogWarning(
                        exception,
                        "Transactional attempt {Attempt} of {MaxAttempts} failed. Retrying after {Delay}",
                        attempt,
                        _options.MaxAttempts,
                        delay);

                    if (delay > TimeSpan.Zero)
                    {
                        await Task.Delay(delay, cancellationToken);
                    }
                }
            }

            throw new InvalidOperationException("The transactional retry loop completed without returning or throwing.");
        }

        private bool ShouldRetry(Exception exception, int attempt, CancellationToken cancellationToken)
        {
            if (attempt >= _options.MaxAttempts)
                return false;

            if (cancellationToken.IsCancellationRequested)
                return false;

            return exception is ConcurrencyConflictException;
        }

        private TimeSpan CalculateDelay(int failedAttempt)
        {
            if (_options.InitalDelay == TimeSpan.Zero)
                return TimeSpan.Zero;

            var exponentialMultiplier = Math.Pow(2, failedAttempt - 1);

            var delayMilliseconds = Math.Min(
                _options.InitalDelay.TotalMilliseconds * exponentialMultiplier,
                _options.MaximumDelay.TotalMilliseconds);

            if (_options.UseJitter)
                delayMilliseconds *= Random.Shared.NextDouble() * 0.5 + 0.75;

            return TimeSpan.FromMilliseconds(delayMilliseconds);
        }
    }
}
