namespace Common.Results
{
    public interface IProblemDetailEnricher
    {
        ProblemDetails Enrich(ProblemDetails problem);
    }
}
