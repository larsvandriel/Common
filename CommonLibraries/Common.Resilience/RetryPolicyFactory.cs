using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Resilience
{
    public static class RetryPolicyFactory
    {
        public static IRetryPolicy CreateDefault(Func<Exception, bool> shouldRetry)
        {
            return Create(RetryOptions.Default, shouldRetry);
        }

        public static IRetryPolicy Create(RetryOptions options, Func<Exception, bool> shouldRetry)
        {
            return new ExponentialBackoffRetryPolicy(options, shouldRetry);
        }
    }
}
