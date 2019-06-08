namespace CleanConnect.Core.Dal.Record
{
    /// <summary>
    /// Indicates that a record can be cached in memory and defines the cache key.
    /// </summary>
    public interface ICacheable
    {
        /// <summary>
        /// The cache key to use.  should be set in the constructor.
        /// </summary>
        string CacheKey { get; }
    }
}