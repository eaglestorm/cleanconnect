using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Messages
{
    public class ProcessNoneResponseMessage: ResponseBase
    {
        /// <summary>
        /// the authorization code to send to the user.
        /// </summary>
        public string Code { get; }
        
        /// <summary>
        /// The uri to call with the response.
        /// </summary>
        public string RedirectUri { get; }
        
        /// <summary>
        /// State parameter passed from the RP.
        /// </summary>
        public string State { get; set; }

        public ProcessNoneResponseMessage(bool success, string code, string redirectUri) : base(success)
        {
            Code = code;
            RedirectUri = redirectUri;
        }

        public ProcessNoneResponseMessage(bool success, Validations errors) : base(success, errors)
        {
            
        }
    }
}