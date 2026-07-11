using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Resilience
{
    public sealed class TransactionalRetryOptions
    {
        public const string SectionName = "Persistence:TransactionalRetry";

        public int MaxAttempts { get; init; } = 3;

        public TimeSpan InitialDelay { get; init; } = TimeSpan.FromMilliseconds(20);

        public TimeSpan MaximumDelay { get; init; } = TimeSpan.FromMilliseconds(250);

        public bool UseJitter { get; init; } = true;
    }
}
