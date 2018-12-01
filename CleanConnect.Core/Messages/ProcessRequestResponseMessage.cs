using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Messages
{
    /// <summary>
    /// The response from the clients authentication request.
    /// Indicates what user interaction is necessary.
    /// </summary>
    public class ProcessRequestResponseMessage: ResponseBase
    {       
        public ProcessRequestResponseMessage(bool success, bool loginRequired, bool consentRequired, bool selectAccount)
        :base(success)
        {
            LoginRequired = loginRequired;
            ConsentRequired = consentRequired;
            SelectAccount = selectAccount;
        }
        
        public ProcessRequestResponseMessage(bool success, ErrorCode error, string redirectUrl)
            :base(success)
        {
            Error = error;
            RedirectUrl = redirectUrl;
        }

        /// <summary>
        /// The user must be prompted to login.
        /// </summary>
        public bool LoginRequired { get; }
        
        /// <summary>
        /// The user must be prompted to give permission to the client application.
        /// </summary>
        public bool ConsentRequired { get; }
        
        /// <summary>
        /// The user has multiple accounts and they must select one.
        /// </summary>
        public bool SelectAccount { get; }

        /// <summary>
        /// Open ID compliant error to return if request fails.
        /// </summary>
        public ErrorCode Error { get; }

        /// <summary>
        /// If the request fails this is the url to redirect to.      
        /// </summary>
        public string RedirectUrl { get;  }
        
        
                
    }
}