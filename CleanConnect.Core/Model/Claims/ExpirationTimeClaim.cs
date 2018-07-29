using System;

namespace CleanConnect.Core.Model.Claims
{
    /// <summary>
    /// A claim that uses a datetime as a value.  
    /// </summary>
    public class ExpirationTimeClaim: Claim<DateTimeOffset>, IClaim
    {
        public ExpirationTimeClaim(string name, string description, DateTimeOffset value) 
            : base(name, description)
        {
            Value = value;
        }

        public override DateTimeOffset Value { get; }
        
        public string GetValueAsString()
        {
            return Value.ToUnixTimeSeconds().ToString();
        }

        /// <summary>
        /// Only allow processing if the current date and time is before the expiration time.
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public bool AllowProcessing()
        {
            return Value.CompareTo(DateTimeOffset.Now) >= 0;
        }
    }
}