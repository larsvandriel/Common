using Common.Messaging.Abstractions.Pipelines;
using Common.Messaging.Abstractions.Requests;
using Common.Results;
using Common.Results.Problems;
using Microsoft.Extensions.Logging;

namespace Common.Messaging.Sync.Pipelines
{
    public sealed class SyncExceptionHandlingBehavior<TRequest>(
        ILogger<SyncExceptionHandlingBehavior<TRequest>> logger) : ISyncRequestPipelineBehavior<TRequest, Result> where TRequest : IRequest<Result>
    {
        public Result Handle(TRequest request, SyncRequestHandlerDelegate<Result> next)
        {
            try
            {
                return next();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "An unexpected error occurred while handling the request of type {RequestType}.", typeof(TRequest).Name);

                return Result.Failure(ProblemDetailsFactory.Unexpected());
            }
        }
    }

    public sealed class SyncExceptionHandlingBehavior<TRequest, TValue>(
        ILogger<SyncExceptionHandlingBehavior<TRequest, TValue>> logger) : ISyncRequestPipelineBehavior<TRequest, Result<TValue>> where TRequest : IRequest<Result<TValue>>
    {
        public Result<TValue> Handle(TRequest request, SyncRequestHandlerDelegate<Result<TValue>> next)
        {
            try
            {
                return next();
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "An unexpected error occurred while handling the request of type {RequestType}.", typeof(TRequest).Name);
                return Result<TValue>.Failure(ProblemDetailsFactory.Unexpected());
            }
        }
    }
}
