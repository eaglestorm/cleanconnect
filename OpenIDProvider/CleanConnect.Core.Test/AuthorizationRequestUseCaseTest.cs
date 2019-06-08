using System;
using System.Collections.Generic;
using CleanConnect.Common.Model.Identity;
using CleanConnect.Core.Messages;
using CleanConnect.Core.Model.Authorization;
using CleanConnect.Core.Model.Client;
using CleanConnect.Core.UseCase;
using NSubstitute;
using Xunit;

namespace CleanConnect.Core.Test
{
    public class AuthorizationRequestUseCaseTest
    {
        private Guid ClientId = Guid.NewGuid();
        private const string ClientSecret = "ClientSecret";
        private const string RedirectUri = "http://google.com/redirecttest";

        /// <summary>
        /// Basic success scenario with minimum data.
        /// </summary>
        [Fact]
        public void UseCaseSuccess()
        {
            var repo = Substitute.For<IClientRepository>();
            repo.Get(Guid.NewGuid()).ReturnsForAnyArgs(new Client(Guid.NewGuid(), "Client","secret",new List<string>()
            {
                RedirectUri
            }));
            

            var authRepo = Substitute.For<IAuthorizationRequestRepository>();
            authRepo.WhenForAnyArgs(x=>x.Save(null)).Do(x=> x.Arg<AuthenticationRequest>().SetIdentity(new LongIdentity(1)));
            
            var message = GetRequest();
            
            var target = new AuthorizationRequestUseCase(repo, authRepo);
            var responseMessage = target.Process(message);
            
            Assert.NotNull(responseMessage);
            Assert.True(responseMessage.Success,"responseMessage.Success"); 
            Assert.True(responseMessage.LoginRequired,"responseMessage.LoginRequired");
            Assert.NotNull(responseMessage.RequestId);
            Assert.True(responseMessage.RequestId.Id > 0,"responseMessage.RequestId.Id > 0");
        }

        /// <summary>
        /// Basic loign scenario when login is requested.
        /// </summary>
        [Fact]
        public void UseCaseLoginSuccess()
        {
            var repo = Substitute.For<IClientRepository>();
            repo.Get(Guid.NewGuid()).ReturnsForAnyArgs(new Client(Guid.NewGuid(), "Client","secret",new List<string>()
            {
                RedirectUri
            }));
            

            var authRepo = Substitute.For<IAuthorizationRequestRepository>();
            authRepo.WhenForAnyArgs(x=>x.Save(null)).Do(x=> x.Arg<AuthenticationRequest>().SetIdentity(new LongIdentity(1)));
            
            var message = GetRequest();
            message.Prompts = "login";
            
            var target = new AuthorizationRequestUseCase(repo, authRepo);
            var responseMessage = target.Process(message);
            
            Assert.NotNull(responseMessage);
            Assert.True(responseMessage.Success,"responseMessage.Success"); 
            Assert.True(responseMessage.LoginRequired,"responseMessage.LoginRequired");
            Assert.NotNull(responseMessage.RequestId);
            Assert.True(responseMessage.RequestId.Id > 0,"responseMessage.RequestId.Id > 0");
        }
        
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void UseCaseNoneFails()
        {
            var repo = Substitute.For<IClientRepository>();
            repo.Get(Guid.NewGuid()).ReturnsForAnyArgs(new Client(Guid.NewGuid(), "Client","secret",new List<string>()
            {
                RedirectUri
            }));
            

            var authRepo = Substitute.For<IAuthorizationRequestRepository>();
            authRepo.WhenForAnyArgs(x=>x.Save(null)).Do(x=> x.Arg<AuthenticationRequest>().SetIdentity(new LongIdentity(1)));
            
            var message = GetRequest();
            message.Prompts = "none";
            
            var target = new AuthorizationRequestUseCase(repo, authRepo);
            var responseMessage = target.Process(message);
            
            Assert.NotNull(responseMessage);
            Assert.False(responseMessage.Success,"responseMessage.Success"); 
            Assert.NotNull(responseMessage.Errors);
            Assert.True(responseMessage.Errors.Count > 0,"responseMessage.Errors.Count > 0");
            Assert.Null(responseMessage.RequestId);
        }

        private ProcessRequestMessage GetRequest()
        {
            return new ProcessRequestMessage()
            {
               Scope = "openid",
                ResponseType = "code",
                ClientId = ClientId,
                RedirectUri = RedirectUri,
                State = "StateValue"
            };
        }
    }
}