using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Requests
{
    public interface IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
    }
}
