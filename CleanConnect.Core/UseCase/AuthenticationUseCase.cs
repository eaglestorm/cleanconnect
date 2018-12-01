using CleanConnect.Common.Contracts;
using CleanConnect.Core.Messages;

namespace CleanConnect.Core.UseCase.Impl
{
    public class AuthenticationUseCase: IUseCase<LoginRequestMessage, LoginResponse>
    {
        public LoginResponse Process(LoginRequestMessage request)
        {
            throw new System.NotImplementedException();
        }
    }
}