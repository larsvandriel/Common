namespace Common.Messaging.Abstractions.PubSub
{
    public interface IAsyncEventBus
    {
        IAsyncDisposable Subscribe<TEvent>(Func<TEvent, CancellationToken, Task> handler);
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default);
    }
}
