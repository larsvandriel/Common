using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public interface IProblemDetailEnricher
    {
        ProblemDetails Enrich(ProblemDetails problem);
    }
}
