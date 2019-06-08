using CleanConnect.Common.Contracts;
using CleanConnect.Core.Messages;

namespace CleanConnect.Core.UseCase
{
    /// <summary>
    /// The user has authenticated and needs to give consent for information to be sent to the third party.
    /// </summary>
    public class RequestConsentUseCase: IUseCase<ConsentRequestMessage,ConsentResponseMessage>
    {
        public ConsentResponseMessage Process(ConsentRequestMessage request)
        {
            throw new System.NotImplementedException();
        }
    }
}