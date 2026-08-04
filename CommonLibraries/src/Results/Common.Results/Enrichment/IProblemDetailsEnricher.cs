using Common.Results.Problems;

namespace Common.Results.Enrichment
{
    public interface IProblemDetailsEnricher
    {
        ProblemDetails Enrich(ProblemDetails problem);
    }
}
