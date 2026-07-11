namespace Common.Persistence.Transactions
{
    public interface ITransactionManager
    {
        Task<ITransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}
