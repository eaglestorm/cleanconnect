namespace CleanConnect.Common.Contracts
{
    /// <summary>
    /// Defines a use case with a request and response.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public interface IUseCase< in TRequest, out TResponse>
     where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Process the use case and return the response.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        TResponse Process(TRequest request);
    }
    
    /// <summary>
    /// Defines a use case with a request but no response.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IUseCase< in TRequest>
        where TRequest : IRequest
    {
        /// <summary>
        /// Process the request.
        /// </summary>
        /// <param name="request"></param>
        void Process(TRequest request);
    }
}