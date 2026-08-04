namespace Common.Messaging.Outbox.Contracts
{
    public interface IOutboxTransport
    {
        Task PublishAsync(OutboxMessage message, CancellationToken cancellationToken = default);
    }
}
