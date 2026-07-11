using Common.Messaging.Abstractions.Requests;

namespace Common.Messaging.Abstractions.Pipelines
{
    public interface ISyncRequestPipelineBehaviour<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        TResult Handle(TRequest request, SyncRequestHandlerDelegate<TResult> next);
    }
}
