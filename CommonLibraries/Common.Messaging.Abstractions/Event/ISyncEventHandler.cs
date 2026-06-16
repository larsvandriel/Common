using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Abstractions.Event
{
    public interface ISyncEventHandler<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}
