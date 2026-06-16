using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Event
{
    public interface ISyncEventDispatcher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
