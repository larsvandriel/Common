using Common.Persistence.Resilience;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace Common.Resilience.Transactional
{
    public sealed class TransactionalRetryPolicy : ITransactionalRetryPolicy
    {
        private readonly TransactionalRetryOptions _options;
        private readonly IReadOnlyCollection<ITransactionalRetryExceptionClassifier> _exceptionClassifiers;
        private readonly ILogger<TransactionalRetryPolicy> _logger;

        public TransactionalRetryPolicy(
            IOptions<TransactionalRetryOptions> options,
            IEnumerable<ITransactionalRetryExceptionClassifier> exceptionClassifiers,
            ILogger<TransactionalRetryPolicy> logger)
        {
            ArgumentNullException.ThrowIfNull(exceptionClassifiers);
            ArgumentNullException.ThrowIfNull(options);

            _options = options.Value;
            _exceptionClassifiers = [.. exceptionClassifiers];
            _logger = logger;

            if (_exceptionClassifiers.Count == 0)
            {
                _logger.LogWarning(
                    "No transactional retry exception classifiers are registered. Transactional exceptions will not be retried.");
            }
        }

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

            return _exceptionClassifiers.Any(classifier => classifier.IsTransient(exception));
        }

        private TimeSpan CalculateDelay(int failedAttempt)
        {
            if (_options.InitialDelay == TimeSpan.Zero)
                return TimeSpan.Zero;

            var exponentialMultiplier = Math.Pow(2, failedAttempt - 1);

            var delayMilliseconds = Math.Min(
                _options.InitialDelay.TotalMilliseconds * exponentialMultiplier,
                _options.MaximumDelay.TotalMilliseconds);

            if (_options.UseJitter)
                delayMilliseconds *= Random.Shared.NextDouble() * 0.5 + 0.75;

            return TimeSpan.FromMilliseconds(delayMilliseconds);
        }
    }
}
