using Common.Messaging.Outbox.Contracts;

namespace Common.Messaging.Outbox.Transactions
{
    public sealed class OutboxEventBuffer : IOutboxEventCollector, IOutboxEventBuffer
    {
        private readonly List<IOutboxEvent> _events = [];

        public void Add(IOutboxEvent eventMessage)
        {
            ArgumentNullException.ThrowIfNull(eventMessage);
            _events.Add(eventMessage);
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
