using System;
using CleanConnect.Common.Model;
using CleanConnect.Common.Model.Errors;
using CleanConnect.Common.Model.Identity;

namespace CleanConnect.Core.Model.User
{
    /// <summary>
    /// The users email and phone number, etc.
    /// </summary>
    public class ContactDetails: Base<LongIdentity>, IValidator
    {        
        /// <summary>
        /// The number of validation errors that have been found.
        /// </summary>
        private Validations _validations = new Validations();

        /// <summary>
        /// Minimum information required.
        /// </summary>
        /// <param name="id">Database Identity</param>
        /// <param name="email"></param>
        public ContactDetails(LongIdentity id, string email, string phoneNumber,DateTimeOffset created, DateTimeOffset modified)
         :base(id,created, modified)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            _validations.Add(ValidateEmail(email).Errors);
        }
        
        public ContactDetails(string email)
        {
            Email = email;
        }
        
        public ContactDetails(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }
        
        /// <summary>
        /// The users email address.
        /// </summary>
        public string Email { get; private set;  }
        
        /// <summary>
        /// Has the email been confirmed.
        /// Ensures the user has access to the given email address.
        /// </summary>
        public bool EmailVerified { get; private set;  }               
        
        /// <summary>
        /// The users preferred phone number 
        /// </summary>
        public string PhoneNumber { get; private set;  }

        public Validations Errors => _validations;

        public bool IsValid()
        {
            return _validations.Count == 0;
        }

        /// <summary>
        /// Change the users email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Validations ChangeEmail(string email)
        {
            var validations = ValidateEmail(email);
            if (validations.Count > 0) return validations;
            Email = email;
            EmailVerified = false;
            return validations;
        }               

        /// <summary>
        /// TODO: finish this.
        /// </summary>
        public void ConfirmEmail()
        {
            EmailVerified = true;
        }

        /// <summary>
        /// Change the users phone number.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Validations ChangePhoneNumber(string phoneNumber)
        {
            var validations = new Validations();
            if (!RegexConstants.PhoneRegex.IsMatch(phoneNumber))
            {
                validations.AddError(ErrorCode.InvalidPhone, "The supplied phone number is not valid.",nameof(PhoneNumber));
            }
            if (validations.Count > 0) return validations;
            PhoneNumber = phoneNumber;
            return validations;
        }

        /// <summary>
        /// Validate the email address.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private Validations ValidateEmail(string email)
        {
            var validations = new Validations();
            if (!RegexConstants.EmailRegex.IsMatch(email))
            {
                validations.AddError(ErrorCode.InvalidEmail, "The supplied email address is not valid.",nameof(Email));
            }

            return validations;
        }
        
        

    }
}