using Common.Messaging.Outbox.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Messaging.Outbox
{
    public sealed class AttributeOutboxEventTypeResolver : IOutboxEventTypeResolver
    {
        public string Resolve(Type eventType)
        {
            ArgumentNullException.ThrowIfNull(eventType);

            var attribute = eventType.GetCustomAttribute<OutboxEventTypeAttribute>()
                ?? throw new InvalidOperationException($"Outbox event type '{eventType.FullName}' does not have an {nameof(OutboxEventTypeAttribute)}.");

            return attribute.Identifier;
        }
    }
}
