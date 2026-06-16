using Common.Messaging.Abstractions.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Pipelines
{
    public interface IRequestPipelineBehavior<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        Task<TResult> HandleAsync(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken = default);
    }
}
