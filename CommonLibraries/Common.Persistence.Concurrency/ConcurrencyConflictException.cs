using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Concurrency
{
    public sealed class ConcurrencyConflictException(string message, Exception? innerException = null) : Exception(message, innerException);
}
