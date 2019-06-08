using CleanConnect.Common.Contracts;
using CleanConnect.Core.Messages;

namespace CleanConnect.Core.UseCase
{
    /// <summary>
    /// The case where the RP has set the prompt to none to check if the user is already authenticated.
    /// </summary>
    public class AuthorizarionNoneRequestUseCase: IUseCase<ProcessNoneRequestMessage, ProcessNoneResponseMessage>
    {
        public ProcessNoneResponseMessage Process(ProcessNoneRequestMessage request)
        {
            throw new System.NotImplementedException();
        }
    }
}