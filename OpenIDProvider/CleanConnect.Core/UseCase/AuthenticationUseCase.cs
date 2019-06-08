using CleanConnect.Common.Contracts;
using CleanConnect.Core.Messages;
using CleanConnect.Core.Model.Authorization;

namespace CleanConnect.Core.UseCase.Impl
{
    public class AuthenticationUseCase: IUseCase<LoginRequestMessage, LoginResponse>
    {
        public LoginResponse Process(LoginRequestMessage request)
        {
            return null;
        }
    }
}