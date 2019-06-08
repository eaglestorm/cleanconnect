using System.Collections.Generic;
using System.Linq;

namespace CleanConnect.Core.Model.Claims
{
    /// <summary>
    /// A collection of claims for a scope.
    /// </summary>
    public class ClaimCollection
    {
        private readonly IList<IClaim> _claims = new List<IClaim>();
        
        public ClaimCollection()
        {
            
        }

        public void Add(IClaim claim)
        {
            _claims.Add(claim);
        }

        public override string ToString()
        {
            return _claims.Aggregate("", (current, claim) => current + claim.Name);
        }
    }
}