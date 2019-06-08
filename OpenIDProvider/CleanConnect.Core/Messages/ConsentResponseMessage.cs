using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Messages
{
    public class ConsentResponseMessage: ResponseBase
    {
        public ConsentResponseMessage(bool success) : base(success)
        {
        }

        public ConsentResponseMessage(bool success, Validations errors) : base(success, errors)
        {
        }
    }
}