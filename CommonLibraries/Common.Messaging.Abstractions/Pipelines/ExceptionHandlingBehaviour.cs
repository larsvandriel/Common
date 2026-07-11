using Common.Messaging.Abstractions.Requests;
using Common.Results;

namespace Common.Messaging.Abstractions.Pipelines
{
    public sealed class ExceptionHandlingBehaviour<TRequest> : IRequestPipelineBehaviour<TRequest, Result> where TRequest : IRequest<Result>
    {
        public async Task<Result> HandleAsync(TRequest request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken = default)
        {
            try
            {
                return await next(cancellationToken);
            }
            catch(Exception exception)
            {
                return Result.Failure(ProblemDetailsFactory.Unexpected(exception, detail: "An unexpected error occurred while handling the request."));
            }
        }
    }

    public sealed class ExceptionHandlingBehaviour<TRequest, TValue> : IRequestPipelineBehaviour<TRequest, Result<TValue>> where TRequest : IRequest<Result<TValue>>
    {
        public async Task<Result<TValue>> HandleAsync(TRequest request, RequestHandlerDelegate<Result<TValue>> next, CancellationToken cancellationToken = default)
        {
            try
            {
                return await next(cancellationToken);
            }
            catch (Exception exception)
            {
                return Result<TValue>.Failure(ProblemDetailsFactory.Unexpected(exception, detail: "An unexpected error occurred while handling the request."));
            }
        }
    }
}
