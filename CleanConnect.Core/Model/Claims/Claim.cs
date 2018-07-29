namespace CleanConnect.Core.Model.Claims
{
    /// <summary>
    /// A claim made about a end-user.
    /// </summary>
    public abstract class Claim<T>
    {
        /// <summary>
        /// Create a claim given the provided values.
        /// </summary>
        /// <remarks>
        /// Not sure about this constructor need to handle dates and other types.
        /// </remarks>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="value"></param>
        public Claim(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        /// <summary>
        /// Name of the claim used in the JWT.
        /// </summary>
        public string Name { get;}
        
        /// <summary>
        /// Readable description of the claim.
        /// </summary>
        public string Description { get; }        

        /// <summary>
        /// The value of the claim
        /// </summary>
        public abstract T Value { get; }
        
    }
}