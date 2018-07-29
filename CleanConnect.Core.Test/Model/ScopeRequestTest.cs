using CleanConnect.Common.Model.Errors;
using CleanConnect.Core.Model;
using CleanConnect.Core.Model.Authorization;
using Xunit;

namespace CleanConnect.Core.Test.Model
{
    public class ScopeRequestTest
    {

        [Fact]
        public void ValidScopeTest()
        {
            var scopes = new ScopesRequest("openid profile");
            Assert.Equal(2, scopes.Count);
            Assert.True(scopes.Contains("openid"));
            Assert.True(scopes.Contains("profile"));
            Assert.True(scopes.IsValid());
        }

        [Fact]
        public void InValidOpenIdMissingTest()
        {
            var scopes = new ScopesRequest("profile");
            Assert.Equal(1, scopes.Count);            
            Assert.False(scopes.IsValid());
            Assert.True(scopes.Errors.Contains(ErrorCode.InvalidScope));
        }
    }
}