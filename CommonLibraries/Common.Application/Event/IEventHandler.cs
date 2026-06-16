using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Event
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
    }
}
