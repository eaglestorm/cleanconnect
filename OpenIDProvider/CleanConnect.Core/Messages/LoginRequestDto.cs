using CleanConnect.Common.Contracts;

namespace CleanConnect.Core.Messages
{
    /// <summary>
    /// A login request by the user.
    /// </summary>
    public class LoginRequestMessage: IRequest<LoginResponse>
    {
        /// <summary>
        /// The username
        /// </summary>
        public string Username { get; set; }
                
        /// <summary>
        /// The password
        /// </summary>
        public string Password { get; set; }
                
        /// <summary>
        /// The device the user is using to authenticate.
        /// </summary>
        public string DeviceId { get; set; }
    }
}