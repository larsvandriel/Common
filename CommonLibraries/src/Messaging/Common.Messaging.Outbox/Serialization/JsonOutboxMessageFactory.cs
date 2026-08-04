using Common.Messaging.Outbox.Contracts;
using System.Text.Json;

namespace Common.Messaging.Outbox.Serialization
{
    public sealed class JsonOutboxMessageFactory(
        JsonSerializerOptions serializerOptions,
        TimeProvider timeProvider,
        IOutboxEventTypeResolver typeResolver) : IOutboxMessageFactory
    {
        public OutboxMessage Create(IOutboxEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event);

            var eventType = @event.GetType();
            var typeName = typeResolver.Resolve(eventType);

            var payload = JsonSerializer.Serialize(@event, eventType, serializerOptions);

            return OutboxMessage.Create(Guid.NewGuid(), timeProvider.GetUtcNow(), typeName, payload);
        }
    }
}
