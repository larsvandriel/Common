using Common.Messaging.Abstractions.Pipelines;
using Common.Messaging.Abstractions.Requests;
using Common.Results;
using Common.Results.Problems;
using Microsoft.Extensions.Logging;

namespace Common.Messaging.Async.Pipelines
{
    public sealed class ExceptionHandlingBehavior<TRequest>(
        ILogger<ExceptionHandlingBehavior<TRequest>> logger) : IRequestPipelineBehavior<TRequest, Result> where TRequest : IRequest<Result>
    {
        public async Task<Result> HandleAsync(TRequest request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken = default)
        {
            try
            {
                return await next(cancellationToken);
            }
            catch(OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch(Exception exception)
            {
                logger.LogError(exception, "An unexpected error occurred while handling the request of type {RequestType}.", typeof(TRequest).Name);

                return Result.Failure(ProblemDetailsFactory.Unexpected());
            }
        }
    }

    public sealed class ExceptionHandlingBehavior<TRequest, TValue>(
        ILogger<ExceptionHandlingBehavior<TRequest, TValue>> logger) : IRequestPipelineBehavior<TRequest, Result<TValue>> where TRequest : IRequest<Result<TValue>>
    {
        public async Task<Result<TValue>> HandleAsync(TRequest request, RequestHandlerDelegate<Result<TValue>> next, CancellationToken cancellationToken = default)
        {
            try
            {
                return await next(cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                throw;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "An unexpected error occurred while handling the request of type {RequestType}.", typeof(TRequest).Name);

                return Result<TValue>.Failure(ProblemDetailsFactory.Unexpected());
            }
        }
    }
}
