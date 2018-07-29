using System;
using CleanConnect.Core.Model.Claims;

namespace CleanConnect.Core.Model
{
    /// <summary>
    /// A claim made about a end-user.
    /// </summary>
    public class GenericClaim: Claim<string>, IClaim
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
        public GenericClaim(string name, string description, string value)
        :base(name,description)
        {            
            Value = value;
        }              

        /// <summary>
        /// The value of this claim.
        /// </summary>
        public override string Value { get; }

        public string GetValueAsString()
        {
            throw new NotImplementedException();
        }
    }
}