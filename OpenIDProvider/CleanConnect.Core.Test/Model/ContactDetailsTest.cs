using CleanConnect.Common.Model.Errors;
using CleanConnect.Core.Model.User;
using Xunit;

namespace CleanConnect.Core.Test.Model
{
    public class ContactDetailsTest
    {
        private const string TestEmail = "test@email.com";
        private const string TestPhone = "0425560633";
        
        [Fact]
        public void CanCreateContactDetails()
        {
            var contactDetails = CreateContactDetails();
            Assert.True(contactDetails.IsValid());
            Assert.Equal(TestEmail,contactDetails.Email);
            Assert.False(contactDetails.EmailVerified);
            Assert.Equal(TestPhone,contactDetails.PhoneNumber);
        }
        
        [Fact]
        public void CanChangeEmail()
        {
            var contactDetails = CreateContactDetails();
            var email = "test2@test.com";
            var validations = contactDetails.ChangeEmail(email);
            Assert.True(contactDetails.IsValid());
            Assert.True(validations.Count == 0);
            Assert.Equal(email,contactDetails.Email);
        }

        [Theory]
        [InlineData("testing")]
        [InlineData("test@")]
        [InlineData("test@test")]
        [InlineData("testing@test.")]
        [InlineData("testing.com")]
        public void TestInvalidEmails(string email)
        {
            var contactDetails = CreateContactDetails();
            var validations = contactDetails.ChangeEmail(email);
            Assert.True(validations.Count > 0);
            Assert.Contains(validations.Errors,x=>x.ErrorCode == ErrorCode.InvalidEmail);
        }

        [Fact]
        public void CanChangePhone()
        {
            var contactDetails = CreateContactDetails();
            var phone = "+61412250522";
            var validations = contactDetails.ChangePhoneNumber(phone);
            Assert.True(contactDetails.IsValid());
            Assert.True(validations.Count == 0);
            Assert.Equal(phone,contactDetails.PhoneNumber);
        }

        [Theory]
        [InlineData("-61412250522")]
        [InlineData("61r12250522")]
        [InlineData("123456789023456")]        
        public void TestInvalidPhone(string phone)
        {
            var contactDetails = CreateContactDetails();
            var validations = contactDetails.ChangePhoneNumber(phone);
            Assert.True(validations.Count > 0);
            Assert.Contains(validations.Errors,x=>x.ErrorCode == ErrorCode.InvalidPhone);
        }

        private ContactDetails CreateContactDetails(bool min = false)
        {            
            return min ? new ContactDetails(TestEmail) : new ContactDetails(TestEmail,TestPhone);
        }
    }
}