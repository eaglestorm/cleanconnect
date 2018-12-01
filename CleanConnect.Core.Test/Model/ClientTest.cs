using System;
using System.Collections.Generic;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Core.Model;
using CleanConnect.Core.Model.Client;
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
            var client = new Client(_clientGuid, "TestClient", ClientSecret, _clientUris);
            Assert.Equal(_clientGuid, client.Id);
            Assert.Equal(ClientSecret, client.Secret);
            Assert.Equal(_clientUris.Count, client.RedirectUris.Count);
        }

        [Fact]
        public void InvalidGuidThrows()
        {
            Assert.Throws<CleanConnectException>(() => new Client(Guid.Empty, "TestClient",ClientSecret, _clientUris));
        }
        
        [Fact]
        public void InvalidSecretThrows()
        {
            Assert.Throws<CleanConnectException>(() => new Client(Guid.Empty, "TestClient","", _clientUris));
        }
        
        [Fact]
        public void InvalidUrlsThrows()
        {
            Assert.Throws<CleanConnectException>(() => new Client(Guid.Empty, "TestClient",ClientSecret, new List<string>()));
        }
    }
}