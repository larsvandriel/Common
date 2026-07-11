namespace Common.Persistence.Resilience.Retry
{
    public interface ITransactionalRetryPolicy
    {
        Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> action, CancellationToken cancellationToken = default);
    }
}
