using Common.Messaging.Outbox.Contracts;
using System.Text.Json;

namespace Common.Messaging.Outbox.Serialization
{
    public sealed class JsonOutboxMessageFactory(
        JsonSerializerOptions serializerOptions,
        TimeProvider timeProvider,
        IOutboxEventTypeResolver typeResolver) : IOutboxMessageFactory
    {
        public OutboxMessage Create(IOutboxEvent eventMessage)
        {
            ArgumentNullException.ThrowIfNull(eventMessage);

            var eventType = eventMessage.GetType();
            var typeName = typeResolver.Resolve(eventType);

            var payload = JsonSerializer.Serialize(eventMessage, eventType, serializerOptions);

            return OutboxMessage.Create(Guid.NewGuid(), timeProvider.GetUtcNow(), typeName, payload);
        }
    }
}
