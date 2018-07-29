using System;
using System.Collections.Generic;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Core.Model;
using Xunit;

namespace CleanConnect.Core.Test.Model
{
    public class ClientTest
    {
        private readonly Guid   _clientGuid   = Guid.NewGuid();
        private const string ClientSecret = "secret";

        private readonly IList<string> _clientUris = new List<string>()
        {
            "https://www.testclient.com/redirect"
        };

        [Fact]
        public void ValidClientTest()
        {
            var client = new Client(_clientGuid, ClientSecret, _clientUris);
            Assert.Equal(_clientGuid, client.Id);
            Assert.Equal(ClientSecret, client.Secret);
            Assert.Equal(_clientUris.Count, client.RedirectUris.Count);
        }

        [Fact]
        public void InvalidGuidThrows()
        {
            Assert.Throws<CleanConnectException>(() => new Client(Guid.Empty, ClientSecret, _clientUris));
        }
        
        [Fact]
        public void InvalidSecretThrows()
        {
            Assert.Throws<CleanConnectException>(() => new Client(Guid.Empty, "", _clientUris));
        }
        
        [Fact]
        public void InvalidUrlsThrows()
        {
            Assert.Throws<CleanConnectException>(() => new Client(Guid.Empty, ClientSecret, new List<string>()));
        }
    }
}