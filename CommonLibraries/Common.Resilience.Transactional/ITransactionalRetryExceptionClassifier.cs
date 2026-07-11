namespace Common.Resilience.Transactional
{
    public interface ITransactionalRetryExceptionClassifier
    {
        bool IsTransient(Exception exception);
    }
}
