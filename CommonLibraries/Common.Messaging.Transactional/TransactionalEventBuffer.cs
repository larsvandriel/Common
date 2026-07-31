using Common.Messaging.Abstractions.Event;

namespace Common.Messaging.Transactional
{
    public class TransactionalEventBuffer : ITransactionalEventCollector, ITransactionalEventBuffer
    {
        private readonly List<IEvent> _events = [];

        public void Add(IEvent @event)
        {
            ArgumentNullException.ThrowIfNull(@event);
            _events.Add(@event);
        }

        public void Clear()
        {
            _events.Clear();
        }

        public IReadOnlyList<IEvent> TakeAll()
        {
            var events = _events.ToArray();
            _events.Clear();
            return events;
        }
    }
}
