using Common.Persistence.Concurrency;

namespace Common.Persistence.Resilience.Retry
{
    public sealed class ConcurrencyConflictRetryExceptionClassifier : ITransactionalRetryExceptionClassifier
    {
        public bool IsTransient(Exception exception)
        {
            ArgumentNullException.ThrowIfNull(exception);

            return exception is ConcurrencyConflictException;
        }
    }
}
