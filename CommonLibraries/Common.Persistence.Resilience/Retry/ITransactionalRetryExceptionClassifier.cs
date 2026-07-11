namespace Common.Persistence.Resilience.Retry
{
    public interface ITransactionalRetryExceptionClassifier
    {
        bool IsTransient(Exception exception);
    }
}
