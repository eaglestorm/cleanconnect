using System;
using CleanConnect.Core.Model.User;
using Xunit;

namespace CleanConnect.Core.Test.Model
{
    public class PersonalDetailsTest
    {
        private const string TestGivenName = "John";
        private const string TestMiddleName = "Frank";
        private const string TestFamilyName = "Smith";
        private const string TestNickname = "jman";
        private readonly DateTime _testBirthDate = new DateTime(1980, 4, 12);
        
        [Fact]
        public void CanCreatePersonalDetails()
        {
            var personalDetails = CreatePersonalDetails();
            Assert.True(personalDetails.IsValid());                        
        }

        [Fact]
        public void CanChangeName()
        {
            var personalDetails = CreatePersonalDetails();
            var validations = personalDetails.ChangeName("James", "Earl", "Jones");
            Assert.True(validations.Count == 0);
            Assert.Equal("James Earl Jones",personalDetails.Name);
        }

        [Fact]
        public void CanChangeNickname()
        {
            var personalDetails = CreatePersonalDetails();
            var validations = personalDetails.ChangeNickname("joe");
            Assert.True(validations.Count == 0);
            Assert.Equal("joe",personalDetails.Nickname);
        }

        [Theory]
        [InlineData("joe.")]
        [InlineData("#joe")]
        [InlineData("")]
        [InlineData(null)]
        public void InvalidNicknameTest(string name)
        {
            var personalDetails = CreatePersonalDetails();
            var validations = personalDetails.ChangeNickname(name);
            Assert.True(validations.Count > 0);
        }

        [Theory]
        [InlineData("joe","james","smith#")]
        [InlineData("joe","james#","smith")]
        [InlineData("joe#","james","smith")]
        [InlineData("joe","james","")]
        [InlineData("joe","james",null)]
        [InlineData(null,"james","smith")]
        [InlineData("joe",null,"smith")]
        public void InvalidNameTest(string given, string middle, string family)
        {
            var personalDetails = CreatePersonalDetails();
            var validations = personalDetails.ChangeName(given, middle, family);
            Assert.True(validations.Count > 0);
        }

        private PersonalDetails CreatePersonalDetails()
        {            
             return new PersonalDetails(TestGivenName, TestFamilyName, _testBirthDate);
        }
    }
}