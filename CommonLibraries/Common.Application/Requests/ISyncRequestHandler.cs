using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Requests
{
    public interface ISyncRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        TResult Handle (TRequest request);
    }
}
