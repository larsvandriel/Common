using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messaging.Outbox
{
    public sealed class OutboxOptions
    {
        public const string SectionName = "Messaging.Outbox";

        public int BatchSize { get; set; } = 100;
        public int MaximumAttempts { get; set; } = 10;
        public TimeSpan InitialRetryDelay { get; set; } = TimeSpan.FromSeconds(10);
        public TimeSpan MaximumRetryDelay { get; set; } = TimeSpan.FromHours(1);
    }
}
