namespace Common.Messaging.Abstractions.Requests
{
    public interface ISyncRequestDispatcher
    {
        TResult Send<TResult>(IRequest<TResult> request);
        TResult Send<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>;
    }
}
