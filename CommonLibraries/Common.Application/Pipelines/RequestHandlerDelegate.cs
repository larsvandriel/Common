using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Pipelines
{
    public delegate Task<TResult> RequestHandlerDelegate<TResult>(CancellationToken cancellationToken = default);
}
