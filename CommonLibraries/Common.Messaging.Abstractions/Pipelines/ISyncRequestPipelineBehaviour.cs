using Common.Messaging.Abstractions.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Pipelines
{
    public interface ISyncRequestPipelineBehaviour<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        TResult Handle(TRequest request, SyncRequestHandlerDelegate<TResult> next);
    }
}
