namespace CleanConnect.Common.Model.Errors
{
    /// <summary>
    /// An error returned to the client with a 400 response code. 
    /// </summary>
    public class ClientError
    {       

        /// <summary>
        /// Create a client error
        /// </summary>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        public ClientError(ErrorCode errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
        
        /// <summary>
        /// The error code for this error.
        /// Should be used by clients when deciding what to do with the error and displaying a message for the error.
        /// </summary>
        public ErrorCode ErrorCode { get; }
        
        /// <summary>
        /// A readable message for the error.
        /// Mostly for developers and generally shouldn't be displayed to the user.
        /// </summary>
        public string Message { get; }
    }
}