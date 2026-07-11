using Common.Persistence.Resilience.Retry;
using Common.Resilience.SqlServer;

namespace Common.Persistence.Resilience.SqlServer
{
    public sealed class SqlServerTransactionalRetryExceptionClassifier : ITransactionalRetryExceptionClassifier
    {
        public bool IsTransient(Exception exception)
        {
            return SqlRetryPolicyFactory.IsTransientSqlException(exception);
        }
    }
}
