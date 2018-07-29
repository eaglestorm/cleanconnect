using System;

namespace CleanConnect.Common.Model
{
    /// <summary>
    /// Allows string values and int values to be associated.
    /// We can return the string but use the int for comparison.
    /// https://stackoverflow.com/questions/424366/string-representation-of-an-enum 
    /// </summary>
    public abstract class TypeSafeEnum
    {
        /// <summary>
        /// Readable name
        /// </summary>
        private readonly string _name;
        
        /// <summary>
        /// Internal value
        /// </summary>
        private readonly int _value;

        /// <summary>
        /// create the type safe enum.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        protected TypeSafeEnum(int value, string name)
        {
            _name = name;
            _value = value;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}