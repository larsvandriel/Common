using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Resilience.Transactional
{
    public interface ITransactionalRetryExceptionClassifier
    {
        bool IsTransient(Exception exception);
    }
}
