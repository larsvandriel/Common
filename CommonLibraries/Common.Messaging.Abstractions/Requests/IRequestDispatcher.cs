using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Requests
{
    public interface IRequestDispatcher
    {
        Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default);
    }
}
