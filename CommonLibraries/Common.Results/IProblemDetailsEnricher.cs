using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Results
{
    public interface IProblemDetailsEnricher
    {
        ProblemDetails Enrich(ProblemDetails problem);
    }
}
