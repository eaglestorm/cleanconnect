using System;

namespace CleanConnect.Common.Model.Errors
{
    /// <summary>
    /// An exception thrown by the application to indicate that the current request should terminate
    /// </summary>
    /// <remarks>
    /// Should not be used for validation.
    /// </remarks>
    public class CleanConnectException: Exception
    {
        public CleanConnectException(
            ErrorCode errorCode,
            string message)
        :base(message)
        {
            ErrorCode = errorCode;
        }
        
        public ErrorCode ErrorCode { get; }
    }
}