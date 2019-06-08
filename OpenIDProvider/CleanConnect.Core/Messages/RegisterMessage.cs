using CleanConnect.Common.Contracts;

namespace CleanConnect.Core.Messages
{
    /// <summary>
    /// User registration message.
    /// </summary>
    public class RegisterMessage: IRequest<RegisterResponseMessage>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string MiddleName { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
}