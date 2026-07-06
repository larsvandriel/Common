using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Requests
{
    public interface IRequestDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default);
        Task<TResult> DispatchAsync<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResult>;
    }
}
