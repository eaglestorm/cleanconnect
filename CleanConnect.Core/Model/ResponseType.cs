using System;
using CleanConnect.Common.Model;

namespace CleanConnect.Core.Model
{
    /// <summary>
    /// Response type requested by the client.
    /// </summary>
    public sealed class ResponseType: TypeSafeEnum
    {
        /// <summary>
        /// authorization code flow.
        /// </summary>
        public static readonly ResponseType Code = new ResponseType(1, "Code"); 
        /// <summary>
        /// Implicit code flow.
        /// </summary>
        public static readonly ResponseType Token = new ResponseType(2, "Token");

        private ResponseType(int value, string name) : base(value, name)
        {
        }
    }
}