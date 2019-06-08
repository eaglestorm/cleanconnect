using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Common.Model.Identity;
using CleanConnect.Core.Messages;
using CleanConnect.Core.Model;
using CleanConnect.Core.Model.Authorization;
using CleanConnect.Core.Model.Client;

namespace CleanConnect.Core.UseCase
{
    public class AuthorizationRequestUseCase: IUseCase<ProcessRequestMessage, ProcessRequestResponseMessage>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAuthorizationRequestRepository _authorization;

        public AuthorizationRequestUseCase(IClientRepository clientRepository, IAuthorizationRequestRepository requestRepository)
        {
            _clientRepository = clientRepository;
            _authorization = requestRepository;
        }
        
        public ProcessRequestResponseMessage Process(ProcessRequestMessage request)
        {
            //This assumes the client has already been validated.
             Validations validations = null;
             string redrectUri = null;
            try
            {
                var client = _clientRepository.Get(request.ClientId);

                AuthenticationRequest authenticationRequest = new AuthenticationRequest(request.Scope,
                    ResponseType.GetFromString(request.ResponseType), client,
                    request.RedirectUri, request.State);
                redrectUri = authenticationRequest.RedirectUri;
                if (authenticationRequest.IsNonePrompt())
                {
                    //This use case doesn't handle this scenario.  
                    new ProcessRequestResponseMessage(false, ErrorCode.ServerError, authenticationRequest.RedirectUri);
                }

                //Don't like this bit.
                if (!string.IsNullOrEmpty(request.Display))
                {
                    authenticationRequest.SetDisplay(request.Display);
                }

                if (!string.IsNullOrEmpty(request.Locales))
                {
                    authenticationRequest.SetLocales(request.Locales);
                }

                if (!string.IsNullOrEmpty(request.Prompts))
                {
                    authenticationRequest.SetPrompts(request.Prompts);
                }

                authenticationRequest.SetMaxAge(request.MaxAge);

                if (authenticationRequest.IsValid())
                {
                    _authorization.Save(authenticationRequest);
                    var response = new ProcessRequestResponseMessage(authenticationRequest.Id, true,
                        authenticationRequest.ShowLogin(), authenticationRequest.ShowConsent(),
                        authenticationRequest.ShowSelectAccount());
                    return response;
                }
                else
                {
                    validations = authenticationRequest.Errors;
                }
            }
            catch (CleanConnectException ex)
            {
                validations = new Validations();
                validations.AddError(ex.ErrorCode,"Unable to process request.");
            }

            return new ProcessRequestResponseMessage(false,validations.FirstOrDefault().ErrorCode, redrectUri);
        }

       
    }
}