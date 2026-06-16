using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Event
{
    public interface IEventDispatcher
    {
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : IEvent;
    }
}
