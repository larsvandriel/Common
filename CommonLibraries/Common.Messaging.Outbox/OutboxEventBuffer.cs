using Common.Messaging.Outbox.Abstractions;

namespace Common.Messaging.Outbox
{
    public sealed class OutboxEventBuffer : IOutboxEventCollector, IOutboxEventBuffer
    {
        private readonly List<IOutboxEvent> _events = [];

        public void Add(IOutboxEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event);
            _events.Add(@event);
        }

        public IReadOnlyCollection<IOutboxEvent> Drain()
        {
            if (_events.Count == 0)
            {
                return [];
            }

            var events = _events.ToArray();
            _events.Clear();

            return events;
        }

        public void Clear()
        {
            _events.Clear();
        }
    }
}
