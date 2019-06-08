namespace CleanConnect.Common.Contracts
{
    /// <summary>
    /// A use case request that has no response.
    /// </summary>
    public interface IRequest
    {
        
    }

    /// <summary>
    /// A use case request that has a response.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface IRequest<out TResponse>
    {
        
    }
}