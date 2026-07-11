using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public sealed class ProblemDetailsEnricher(IEnumerable<IProblemDetailEnricher> enrichers) : IProblemDetailsEnricher
    {
        public ProblemDetails Enrich(ProblemDetails problem)
        {
            foreach (var enricher in enrichers)
            {
                problem = enricher.Enrich(problem);
            }

            return problem;
        }
    }
}
