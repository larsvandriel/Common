using Common.Messaging.Abstractions.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Messaging.Sync
{
    public sealed class SyncRequestDispatcher(IServiceProvider serviceProvider) : ISyncRequestDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public TResult Send<TResult>(IRequest<TResult> request)
        {
            ArgumentNullException.ThrowIfNull(request);

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResult));

            dynamic handler = _serviceProvider.GetRequiredService(handlerType);

            return handler.Handle((dynamic)request);
        }
    }
}
