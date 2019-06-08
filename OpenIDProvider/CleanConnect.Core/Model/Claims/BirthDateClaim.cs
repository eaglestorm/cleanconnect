using System;

namespace CleanConnect.Core.Model.Claims
{
    /// <summary>
    /// A date that doesn't care about timezone, i.e. birthdate.
    /// </summary>
    /// <remarks>
    /// This should possibly just be a BirthDateClaim
    /// </remarks>
    public class BirthDateClaim: Claim<DateTime>, IClaim
    {
        public BirthDateClaim(string name, string description, DateTime value) : base(name, description)
        {
            Value = value;
        }

        public override DateTime Value { get; }
        
        public string GetValueAsString()
        {
            return Value.ToString("yyyy-MM-dd");
        }
    }
}