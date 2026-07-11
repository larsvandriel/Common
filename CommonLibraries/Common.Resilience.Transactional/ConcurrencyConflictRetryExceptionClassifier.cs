using Common.Persistence.Concurrency;

namespace Common.Resilience.Transactional
{
    public sealed class ConcurrencyConflictRetryExceptionClassifier : ITransactionalRetryExceptionClassifier
    {
        public bool IsTransient(Exception exception)
        {
            return exception is ConcurrencyConflictException;
        }
    }
}
