using Common.Messaging.Abstractions.Event;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Sync
{
    public sealed class SyncEventDispatcher(IServiceProvider serviceProvider) : ISyncEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            ArgumentNullException.ThrowIfNull(@event);

            var handlers = _serviceProvider.GetServices<ISyncEventHandler<TEvent>>();

            foreach (var handler in handlers)
            {
                handler.Handle(@event);
            }
        }
    }
}
