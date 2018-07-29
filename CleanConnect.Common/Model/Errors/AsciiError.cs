namespace CleanConnect.Common.Model.Errors
{
    /// <summary>
    /// Errors reeturned to the client that are compliant with the spec but not very helpful.
    /// </summary>
    /// <remarks>
    /// These are the external errors where as the error code is the internal error that is logged.
    /// These errors leak less info.
    /// </remarks>
    public sealed class AsciiError
    {        
        /// <summary>
        /// A UI is required to proceed further but the authorisation request has requested that one not be shown.
        /// </summary>
        public static readonly AsciiError InteractionRequired = new AsciiError("interaction_required");
        
        /// <summary>
        /// Authentication required but the authentication request cannot proceed to login.
        /// </summary>
        public static readonly AsciiError LoginRequired = new AsciiError("login_required");
        
        /// <summary>
        /// The end-user is required to select from multiple authenticated sessions but the end-user did not select a session.
        /// </summary>
        public static readonly AsciiError AccountSelectionRequired = new AsciiError("account_selection_required");
        
        /// <summary>
        /// End-user consent is required but could not be obtained.
        /// </summary>
        public static readonly AsciiError ConsentRequired = new AsciiError("consent_required");
        
        /// <summary>
        /// The request uri in the authorization request returns an error or contains invalid data.
        /// </summary>
        public static readonly AsciiError InvalidRequestUri = new AsciiError("invalid_request_uri");
        
        /// <summary>
        /// The request prameter contains an invalid request object.
        /// </summary>
        public static readonly AsciiError InvalidRequestObject = new AsciiError("invalid_request_object");
        
        /// <summary>
        /// The request parameter is not supported.
        /// </summary>
        public static readonly AsciiError RequestNotSupported = new AsciiError("request_not_supported");
        
        /// <summary>
        /// The use of the request uri is not supported.
        /// </summary>
        public static readonly AsciiError RequestUriNotSupported = new AsciiError("request_uri_not_supported");
        
        /// <summary>
        /// User Registration is not supported.
        /// </summary>
        public static readonly AsciiError RegistrationNotSupported = new AsciiError("registration_not_supported");
        
        /// <summary>
        /// The request contains missing or invalid parameters.
        /// </summary>
        public static readonly AsciiError InvalidRequest = new AsciiError("invalid_request");
        
        /// <summary>
        /// The client is not authorized to request an authorization code using the requested method.
        /// </summary>
        public static readonly AsciiError UnauthorizedClient = new AsciiError("unauthorized_client");
        
        /// <summary>
        /// The resource owner or authorization server denied the request.
        /// </summary>
        public static readonly AsciiError AccessDenied = new AsciiError("access_denied");
        
        /// <summary>
        /// The authorization server does not support obtaining an authorization code using this method.
        /// </summary>
        public static readonly AsciiError UnsopportedResponseType = new AsciiError("unsupported_response_type");
        
        /// <summary>
        /// The requested scope is invalid, unknown or malformed.
        /// </summary>
        public static readonly AsciiError InvalidScope = new AsciiError("invalid_scope");
        
        /// <summary>
        /// Unexpected server error.
        /// </summary>
        public static readonly AsciiError ServerError = new AsciiError("server_error");
        
        /// <summary>
        /// The authorization server is temporarily unavailable.
        /// </summary>
        public static readonly AsciiError TemporarilyUnavailable = new AsciiError("temporarily_unavailable");

        private AsciiError(string code)
        {
            Code = code;
        }
        
        /// <summary>
        /// Code returned to the RP.
        /// </summary>
        public string Code { get; }
    }
}