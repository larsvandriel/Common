using Common.Messaging.Abstractions.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Messaging.Async.Requests
{
    public sealed class RequestDispatcher(IServiceProvider serviceProvider) : IRequestDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(request);

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResult));

            dynamic handler = _serviceProvider.GetRequiredService(handlerType);

            return handler.HandleAsync((dynamic)request, cancellationToken);
        }
    }
}
