namespace CleanConnect.Core.Model
{
    /// <summary>
    /// Becase bloody generics and bloody collections.
    /// </summary>
    /// <remarks>
    /// Can't really think of a nicer way to do this for the claim collection without reverting to objects.
    /// </remarks>
    public interface IClaim
    {
        /// <summary>
        /// Name of the claim used in the JWT.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Readable description of the claim.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Get the value as a string.
        /// </summary>
        /// <returns></returns>
        string GetValueAsString();
    }
}