using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Model.Authorization
{
    /// <summary>
    /// Indicates how the RP has requested the end user be prompted for login and consent.
    /// </summary>
    public class Prompts: IValidator
    {
        private readonly IList<Prompt> _prompts = new List<Prompt>();
        
        private Validations _validations = new Validations();

        public Prompts(string prompts)
        {
            SetPrompts(prompts);
            if (_prompts.Contains(Prompt.None) && _prompts.Count != 1)
            {
                _validations.AddError(ErrorCode.InvalidNonePrompt, "The none prompt most be the only prompt if specified.");
            }
        }

        public int Count => _prompts.Count;

        public bool Contains(Prompt prompt)
        {
            return _prompts.Contains(prompt);
        }

        /// <summary>
        /// Returns false if the none prompt has been specified.
        /// </summary>
        /// <returns></returns>
        public bool CanShowLoginAndConsent()
        {
            return _prompts.Contains(Prompt.None);
        }
        
        /// <summary>
        /// set the prompts given the space delimitted string.
        /// </summary>
        /// <param name="prompts"></param>
        private void SetPrompts(string prompts)
        {
            prompts = prompts.Trim();
            if (!prompts.Contains(' '))
            {
                //only one prompt.
                AddPrompt(prompts);
            }
            else
            {
                var promptsList = prompts.Split(' ');
                foreach (var prp in promptsList)
                {
                    AddPrompt(prp);
                }
            }
        }
        
        private void AddPrompt(string prompt)
        {
            if (Prompt.Consent.ToString() == prompt)
            {
                _prompts.Add(Prompt.Consent);
            }
            else if (Prompt.Login.ToString() == prompt)
            {
                _prompts.Add(Prompt.Login);
            }
            else if (Prompt.None.ToString() == prompt)
            {
                _prompts.Add(Prompt.None);
            }
            else if (Prompt.Consent.ToString() == prompt)
            {
                _prompts.Add(Prompt.Consent);
            }
            else if (Prompt.SelectAccount.ToString() == prompt)
            {
                _prompts.Add(Prompt.SelectAccount);
            }
            else
            {
                _validations.AddError(ErrorCode.InvalidPrompt, $"{prompt} is not a valid value for a prompt");
            }
        }

        public bool IsValid()
        {
            return _validations.Count == 0;
        }

        public Validations Errors => _validations;
    }
}