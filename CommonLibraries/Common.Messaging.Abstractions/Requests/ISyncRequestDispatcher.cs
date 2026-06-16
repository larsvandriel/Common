using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Requests
{
    public interface ISyncRequestDispatcher
    {
        TResult Send<TResult>(IRequest<TResult> request);
    }
}
