using System;
using System.Collections.Generic;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Core.Model;
using CleanConnect.Core.Model.Authorization;
using Xunit;

namespace CleanConnect.Core.Test.Model
{
    public class AuthenticationRequestTest
    {
        private const string State = "asdfasdfasdfasdfsdf";
        private const string Scopes = "openid profile";
        private const string Redirect = "https://www.testclient.com/redirect";
        private const string Secret = "secret";
        
        [Fact]
        public void ValidAuthenticationRequestTest()
        {
            var request = new AuthenticationRequest(Scopes,
                ResponseType.Code,
                GetClient(),
                Redirect,
                State);
            
            Assert.True(request.IsValid());
            Assert.Equal(State,State);
            Assert.Equal(Scopes,request.Scopes.ToString());
            Assert.Equal(Secret,request.Client.Secret);
            Assert.Equal(Redirect,request.RedirectUri);
        }

        [Fact]
        public void InvalidRedirectInvalidRequest()
        {
            var request = new AuthenticationRequest("openid profile",
                ResponseType.Code,
                GetClient(),
                "https://www.testclient.com/invalid",
                State);
            
            Assert.False(request.IsValid());
            Assert.Equal(1,request.Errors.Count);
            Assert.True(request.Errors.Contains(ErrorCode.InvalidRedirectUri));
        }

        [Fact]
        public void InvalidScopeInvalidRequest()
        {
            var request = new AuthenticationRequest("profile",
                ResponseType.Code,
                GetClient(),
                "https://www.testclient.com/invalid",
                State);
            
            Assert.False(request.IsValid());
            Assert.True(request.Errors.Contains(ErrorCode.InvalidScope));
        }

        [InlineData("page")]
        [InlineData("popup")]
        [InlineData("touch")]
        [InlineData("wap")]
        [Theory]
        public void CanSetDisplay(string display)
        {
            var request = GetValidRequest();
            request.SetDisplay(display);
            Assert.True(request.IsValid());
        }

        [Fact]
        public void InvalidDisplayInvalidRequest()
        {
            var request = GetValidRequest();
            request.SetDisplay("invalid");
            Assert.False(request.IsValid());
        }

        [InlineData("none")]
        [InlineData("login consent")]
        [InlineData("login")]
        [InlineData("login select_account")]
        [InlineData("login consent select_account")]
        [Theory]
        public void CanSetPrompt(string prompts)
        {
            var request = GetValidRequest();
            request.SetPrompts(prompts);
            Assert.True(request.IsValid());
            Assert.True(request.Prompts.Count >= 1);            
        }

        [Fact]
        public void InvalidNonePromptInvalidRequest()
        {
            var request = GetValidRequest();
            request.SetPrompts("none login");
            Assert.False(request.IsValid());
            Assert.True(request.Errors.Contains(ErrorCode.InvalidNonePrompt));
        }
        
        [Fact]
        public void InvalidPromptInvalidRequest()
        {
            var request = GetValidRequest();
            request.SetPrompts("login invalid");
            Assert.False(request.IsValid());
            Assert.True(request.Errors.Contains(ErrorCode.InvalidPrompt));
        }

        [Fact]
        public void CanSetMaxAge()
        {
            //TODO: check existing session.
            var request = GetValidRequest();
            request.SetMaxAge(24 * 60 * 60);
            Assert.True(request.IsValid());
        }
        
        private Client GetClient()
        {
            return new Client(Guid.NewGuid(), Secret, new List<string>()
            {
                Redirect
            });
        }

        private AuthenticationRequest GetValidRequest()
        {
            return new AuthenticationRequest(Scopes,
                ResponseType.Code,
                GetClient(),
                Redirect,
                State);
        }
    }
}