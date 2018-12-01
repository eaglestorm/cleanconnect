using System;
using System.IO;
using AutoMapper;
using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Core.Messages;
using CleanConnect.Core.UseCase;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CleanConnect.Web.Controllers
{
    public class AuthorizeController : Controller
    {
        private readonly IUseCase<ProcessRequestMessage, ProcessRequestResponseMessage> _authenticationUseCase;
        private readonly IMapper _mapper;

        public AuthorizeController(IUseCase<ProcessRequestMessage, ProcessRequestResponseMessage> authenticationUseCase, IMapper mapper)
        {
            _authenticationUseCase = authenticationUseCase;
            _mapper = mapper;
        }
        
        public IActionResult Index(AuthenticationRequestDto dto)
        {
            var message = _mapper.Map<ProcessRequestMessage>(dto);
            var loginFlow = _authenticationUseCase.Process(message);
                        
            if (loginFlow.Success && loginFlow.LoginRequired)
            {
                return View("Login");
            }
            //if login is not required as the are already authenticated but consent is required.
            if(loginFlow.Success && loginFlow.ConsentRequired)
            {
                return View("Consent");
            }

            //login and consent aren't required but they have multiple accounts.
            if (loginFlow.Success && loginFlow.SelectAccount)
            {
                return View("SelectAccount");
            }
            
            //response was not successful return an error.
            return Redirect(BuildRedirect(loginFlow, dto));
        }

        private string BuildRedirect(ProcessRequestResponseMessage responseMessage, AuthenticationRequestDto dto)
        {
            UriBuilder uriBuilder = new UriBuilder(responseMessage.RedirectUrl);
            QueryBuilder queryBuilder = new QueryBuilder();
            queryBuilder.Add("error",responseMessage.Error.AsciiCode.Code);
            queryBuilder.Add("error_description",responseMessage.Error.Message);
            queryBuilder.Add("state",dto.State);
            uriBuilder.Query = queryBuilder.ToQueryString().Value;
            return uriBuilder.Uri.AbsoluteUri;
        }
    }
}