using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Messages
{
    public class RegisterResponseMessage: ResponseBase
    {
        public RegisterResponseMessage(bool success) : base(success)
        {
        }

        public RegisterResponseMessage(bool success, Validations errors) : base(success, errors)
        {
        }
    }
}