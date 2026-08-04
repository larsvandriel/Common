using Common.Messaging.Outbox.Contracts;
using System.Reflection;

namespace Common.Messaging.Outbox.Serialization
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
