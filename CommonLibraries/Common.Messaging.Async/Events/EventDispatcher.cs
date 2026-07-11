using Common.Messaging.Abstractions.Event;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Messaging.Async.Events
{
    public sealed class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            ArgumentNullException.ThrowIfNull(@event);

            var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                await handler.HandleAsync(@event, cancellationToken);
            }
        }
    }
}
