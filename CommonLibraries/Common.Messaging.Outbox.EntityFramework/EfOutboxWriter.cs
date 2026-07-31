using Common.Messaging.Outbox.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox.EntityFramework
{
    public sealed class EfOutboxWriter<TDbContext>(TDbContext dbContext) : IOutboxWriter where TDbContext: DbContext
    {
        public void Add(OutboxMessage message)
        {
            ArgumentNullException.ThrowIfNull(message);

            dbContext.Set<OutboxMessage>().Add(message);
        }

        public void AddRange(IReadOnlyCollection<OutboxMessage> messages)
        {
            ArgumentNullException.ThrowIfNull(messages);

            dbContext.Set<OutboxMessage>().AddRange(messages);
        }
    }
}
