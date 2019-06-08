using CleanConnect.Common.Model;

namespace CleanConnect.Core.Model.Authorization
{
    /// <summary>
    /// Defines how the authorization server behaves depending on wether the user is already authenticated or not.
    /// </summary>
    public class Prompt: TypeSafeEnum
    {
        /// <summary>
        /// The authorization server must not display any authorization or consent and an error is returned if the user is not authenticated
        /// and haven't given consent for all the requested claims already.
        /// </summary>
        /// <remarks>
        /// Can be used to check for authentication and/or consent. 
        /// </remarks>
        public static readonly Prompt None = new Prompt(1, "none");
        
        /// <summary>
        /// Prompt for re-authentication.  A login_required error should be return if this fails.
        /// </summary>
        public static readonly Prompt Login = new Prompt(2, "login");
        
        /// <summary>
        /// Prompt for consent.  A consent_required error should be returned if this fails.
        /// </summary>
        public static readonly Prompt Consent = new Prompt(3, "consent");        
        
        /// <summary>
        /// The user should be prompted to select a user account.  Used when the user has multiple accounts they could use to authenticate.
        /// </summary>
        public static readonly Prompt SelectAccount = new Prompt(3, "select_account");
        
        
        private Prompt(int value, string name) : base(value, name)
        {
        }
    }
}