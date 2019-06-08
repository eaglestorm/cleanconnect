using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Messages
{
    public class LoginResponse: ResponseBase
    {
        public LoginResponse(bool success) : base(success)
        {
        }

        public LoginResponse(bool success, Validations errors) : base(success, errors)
        {
        }
    }
}