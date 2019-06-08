using System;
using System.Collections;
using System.Collections.Generic;
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
        public static readonly ResponseType Code = new ResponseType(1, "code"); 
        /// <summary>
        /// Implicit code flow.
        /// </summary>
        public static readonly ResponseType Token = new ResponseType(2, "token");

        private ResponseType(int value, string name) : base(value, name)
        {
            
        }

        /// <summary>
        /// convert a string to the correct type.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static ResponseType GetFromString(string code)
        {
            if (code == Code.Name)
            {
                return Code;
            }

            if (code == Token.Name)
            {
                return Token;
            }

            return null;
        }

        public static ResponseType GetFromInt(int value)
        {
            if (value == Code.Value)
            {
                return Code;
            }

            if (value == Token.Value)
            {
                return Token;
            }

            return null;
        }
    }
}