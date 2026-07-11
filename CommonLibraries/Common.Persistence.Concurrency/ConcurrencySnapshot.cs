using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Concurrency
{
    public sealed record ConcurrencySnapshot<T>(T Value, ConcurrencyToken Token);
}
