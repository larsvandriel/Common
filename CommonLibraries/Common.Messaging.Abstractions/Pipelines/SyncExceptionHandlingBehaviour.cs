using Common.Messaging.Abstractions.Requests;
using Common.Results;

namespace Common.Messaging.Abstractions.Pipelines
{
    public sealed class SyncExceptionHandlingBehaviour<TRequest> : ISyncRequestPipelineBehaviour<TRequest, Result> where TRequest : IRequest<Result>
    {
        public Result Handle(TRequest request, SyncRequestHandlerDelegate<Result> next)
        {
            try
            {
                return next();
            }
            catch (Exception exception)
            {
                return Result.Failure(ProblemDetailsFactory.Unexpected(exception, detail: "An unexpected error occurred while handling the request."));
            }
        }
    }

    public sealed class  SyncExceptionHandlingBehaviour<TRequest, TValue> : ISyncRequestPipelineBehaviour<TRequest, Result<TValue>> where TRequest : IRequest<Result<TValue>>
    {
        public Result<TValue> Handle(TRequest request, SyncRequestHandlerDelegate<Result<TValue>> next)
        {
            try
            {
                return next();
            }
            catch (Exception exception)
            {
                return Result<TValue>.Failure(ProblemDetailsFactory.Unexpected(exception, detail: "An unexpected error occurred while handling the request."));
            }
        }
    }
}
