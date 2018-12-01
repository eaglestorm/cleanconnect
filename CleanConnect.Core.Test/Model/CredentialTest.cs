using CleanConnect.Core.Model.User;
using Xunit;

namespace CleanConnect.Core.Test.Model
{
    public class CredentialTest
    {
        private const string TestUsername = "user1@testing.com";
        private const string TestPassword = "Password1!";
        
        [Fact]
        public void CanCreateCredentialTest()
        {
            var credential = CreateCredential(); 
            
            Assert.NotNull(credential);
            Assert.NotNull(credential.HashedPassword);
            Assert.Equal(TestUsername,credential.Username);
            Assert.NotNull(credential.Salt);
            Assert.True(credential.IsValid());
        }

        [Fact]
        public void CanValidatePassword()
        {
            var credential = CreateCredential();
            
            Assert.True(credential.ValidatePassword(TestPassword));
        }

        [Fact]
        public void ValidatePasswordFails()
        {
            var credential = CreateCredential();
            
            Assert.False(credential.ValidatePassword("Password2!"));
        }

        [Fact]
        public void CanChangePassword()
        {
            var credential = CreateCredential();

            var before = credential.HashedPassword;
            
            var result = credential.ChangePassword(TestPassword,"Testing1!","Testing1!");
            
            Assert.True(result.Count == 0);
            Assert.NotEqual(before,credential.HashedPassword);            
        }
        
        [Fact]
        public void CanChangePasswordFails()
        {
            var credential = CreateCredential();
            
            credential.ChangePassword(TestPassword,"Testing1!","Testing2!");
            
            Assert.False(credential.IsValid());
            Assert.True(credential.Errors.Count > 0);
        }
        
        [Theory]
        [InlineData("Pas1!")]   
        [InlineData("Passw1")]
        [InlineData("password1!")]
        [InlineData("Password!")]
        [InlineData("Passwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwword1!")]
        public void InvalidPasswords(string password)
        {
            var credential = CreateCredential();

            var before = credential.HashedPassword;
            
            var result = credential.ChangePassword(TestPassword,password,password);
            Assert.True(result.Count > 0);
            Assert.Equal(before,credential.HashedPassword);
        }

        private Credential CreateCredential()
        {
            return new Credential(TestUsername, TestPassword, TestPassword);
        }
        
        
    }
}