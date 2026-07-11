using Common.Messaging.Abstractions.Requests;

namespace Common.Messaging.Abstractions.Pipelines
{
    public interface IRequestPipelineBehaviour<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        Task<TResult> HandleAsync(TRequest request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken = default);
    }
}
