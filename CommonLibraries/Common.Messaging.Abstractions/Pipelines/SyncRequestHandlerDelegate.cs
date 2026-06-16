using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Pipelines
{
    public delegate TResult SyncRequestHandlerDelegate<TResult>();
}
