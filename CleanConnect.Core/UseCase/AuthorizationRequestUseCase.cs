using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Core.Messages;
using CleanConnect.Core.Model;
using CleanConnect.Core.Model.Authorization;
using CleanConnect.Core.Model.Client;

namespace CleanConnect.Core.UseCase
{
    public class AuthorizationRequestUseCase: IUseCase<ProcessRequestMessage, ProcessRequestResponseMessage>
    {
        private readonly IClientRepository _clientRepository;

        public AuthorizationRequestUseCase(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public ProcessRequestResponseMessage Process(ProcessRequestMessage request)
        {
            var client = _clientRepository.Get(request.ClientId);
            if (client == null)
            {
                //TODO: should be just swallow this?
                return new ProcessRequestResponseMessage(false, ErrorCode.InvalidRequest,request.RedirectUri);
            }
            AuthenticationRequest authenticationRequest = new AuthenticationRequest(request.Scope,ResponseType.GetFromString(request.ResponseType),client, 
                request.RedirectUri,request.State);
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
                var response = new ProcessRequestResponseMessage(true,
                    authenticationRequest.Prompts.Contains(Prompt.Login),
                    authenticationRequest.Prompts.Contains(Prompt.Consent),
                        authenticationRequest.Prompts.Contains(Prompt.SelectAccount));
                
                return response;
            }
            
            return new ProcessRequestResponseMessage(false, authenticationRequest.Errors.FirstOrDefault().ErrorCode,authenticationRequest.RedirectUri);
        }

       
    }
}