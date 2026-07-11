using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Persistence.Resilience
{
    public class TransactionalRetryOptionsValidator : IValidateOptions<TransactionalRetryOptions>
    {
        public ValidateOptionsResult Validate(string? name, TransactionalRetryOptions options)
        {
            var errors = new List<string>();

            if (options.MaxAttempts < 1)
                errors.Add($"{nameof(options.MaxAttempts)} must be at least 1.");

            if (options.InitialDelay < TimeSpan.Zero)
                errors.Add($"{nameof(options.InitialDelay)} cannot be negative.");

            if (options.MaximumDelay < TimeSpan.Zero)
                errors.Add($"{nameof(options.MaximumDelay)} cannot be nagative.");

            if (options.MaximumDelay < options.InitialDelay)
                errors.Add($"{nameof(options.MaximumDelay)} must be greater than or equal to {nameof(options.InitialDelay)}.");

            return errors.Count == 0 ? ValidateOptionsResult.Success : ValidateOptionsResult.Fail(errors);
        }
    }
}
