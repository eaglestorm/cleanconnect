using System.Linq;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Core.Model;
using CleanConnect.Core.Model.Authorization;
using Xunit;

namespace CleanConnect.Core.Test.Model
{
    public class PromptsTest
    {

        [Fact]
        public void ValidPromptsTest()
        {
            var prompts = new Prompts("login consent select_account");
            Assert.Equal(3, prompts.Count);
            Assert.True(prompts.Contains(Prompt.Login));
            Assert.True(prompts.Contains(Prompt.Consent));
            Assert.True(prompts.Contains(Prompt.SelectAccount));
            Assert.True(prompts.IsValid());
        }

        [Fact]
        public void ValidNonePromptTest()
        {
            var prompts = new Prompts("none");
            Assert.Equal(1, prompts.Count);
            Assert.True(prompts.Contains(Prompt.None));            
            Assert.True(prompts.IsValid());
        }

        [Fact]
        public void InvalidNonePromptTest()
        {
            var prompts = new Prompts("none login");
            Assert.Equal(2, prompts.Count);
            Assert.True(prompts.Contains(Prompt.None));            
            Assert.False(prompts.IsValid());
            Assert.True(prompts.Errors.Contains(ErrorCode.InvalidNonePrompt));
        }
        
        [Fact]
        public void InValidPromptTest()
        {
            var prompts = new Prompts("test");
            Assert.Equal(0, prompts.Count);            
            Assert.False(prompts.IsValid());
            Assert.True(prompts.Errors.Contains(ErrorCode.InvalidPrompt));
        }
    }
}