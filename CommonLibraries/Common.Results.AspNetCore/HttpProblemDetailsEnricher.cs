using Microsoft.AspNetCore.Http;

namespace Common.Results.AspNetCore
{
    public class HttpProblemDetailsEnricher (IHttpContextAccessor) : IProblemDetailEnricher
    {

    }
}
