using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Transactions
{
    public sealed class PostCommitException(string message, Exception innerException) : Exception(message, innerException);
}
