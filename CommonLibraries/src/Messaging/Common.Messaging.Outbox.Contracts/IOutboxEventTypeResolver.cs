namespace Common.Messaging.Outbox.Contracts
{
    public interface IOutboxEventTypeResolver
    {
        string Resolve(Type eventType);
    }
}
