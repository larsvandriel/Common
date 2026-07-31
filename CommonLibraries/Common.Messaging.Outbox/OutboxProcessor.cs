using Common.Messaging.Outbox.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Common.Messaging.Outbox
{
    public sealed class OutboxProcessor(
        IOutboxStore store,
        IOutboxTransport transport,
        TimeProvider timeProvider,
        IOptions<OutboxOptions> options,
        ILogger<OutboxProcessor> logger) : IOutboxProcessor
    {
        private readonly OutboxOptions _options = options.Value;

        public async Task<int> ProcessAsync(CancellationToken cancellationToken = default)
        {
            var messages = await store.GetPendingAsync(_options.BatchSize, timeProvider.GetUtcNow(), cancellationToken);

            var publishedCount = 0;

            foreach (var message in messages)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (await ProcessMessageAsync(message, cancellationToken))
                {
                    publishedCount++;
                }
            }

            return publishedCount;
        }

        private async Task<bool> ProcessMessageAsync(OutboxMessage message, CancellationToken cancellationToken)
        {
            try
            {
                await transport.PublishAsync(message, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception publishException)
            {
                await RecordPublishFailureAsync(message, publishException);

                return false;
            }

            message.MarkPublished(timeProvider.GetUtcNow());

            try
            {
                await store.SaveChangesAsync(CancellationToken.None);
                return true;
            }
            catch (Exception persistenceException)
            {
                logger.LogCritical(
                    persistenceException,
                    "Outbox message {OutboxMessageId} was published, but its published status could not be persisted. The message may be published again.",
                    message.Id);

                throw;
            }
        }

        private async Task RecordPublishFailureAsync(OutboxMessage message, Exception publishException)
        {
            var attemptedAtUtc = timeProvider.GetUtcNow();
            var nextAttemptNumber = message.AttemptCount + 1;
            var deadLetter = nextAttemptNumber >= _options.MaximumAttempts;

            DateTimeOffset? nextAttemptAtUtc = deadLetter ? null : attemptedAtUtc + CalculateRetryDelay(nextAttemptNumber);
            
            message.MarkFailed(attemptedAtUtc, publishException.Message, nextAttemptAtUtc, deadLetter);

            try
            {
                await store.SaveChangesAsync(CancellationToken.None);
            }
            catch (Exception persistenceException)
            {
                logger.LogError(
                    persistenceException,
                    "Persisting the failed outbox attempt for message {OutboxMessageId} failed.",
                    message.Id);

                throw;
            }

            logger.LogError(publishException, "Publishing outbox message {OutboxMessageId} failed.", message.Id);
        }

        private TimeSpan CalculateRetryDelay(int attemptNumber)
        {
            var multiplier = Math.Pow(2, attemptNumber - 1);

            var delayTicks = Math.Min(_options.InitialRetryDelay.Ticks * multiplier, _options.MaximumRetryDelay.Ticks);

            return TimeSpan.FromTicks((long)delayTicks);
        }
    }
}
