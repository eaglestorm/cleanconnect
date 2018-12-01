using System;
using System.ComponentModel.DataAnnotations;
using CleanConnect.Common.Model;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Model.User
{
    /// <summary>
    /// A users personal details like name date of birth, etc.
    /// </summary>
    public class PersonalDetails: Base<long>, IValidator
    {        
        /// <summary>
        /// The number of validation errors that have been found.
        /// </summary>
        private readonly Validations _validations = new Validations();
                
        public PersonalDetails(string givenName, string familyName, DateTime dateOfBirth)
        {
            GivenName = givenName;
            FamilyName = familyName;
            DateOfBirth = dateOfBirth;
            Validate();
        }
        
        
        public PersonalDetails(long id, string givenName, string middleName, string familyName, string nickname, DateTime dateOfBirth,  DateTimeOffset created, DateTimeOffset modified)
        :base(id, created, modified)
        {            
            GivenName = givenName;
            FamilyName = familyName;
            DateOfBirth = dateOfBirth;
            MiddleName = middleName;            
            HasMiddleName = true;
            Nickname = nickname; 
            Validate();
        }
        
        /// <summary>
        /// The users first or given name
        /// </summary>
        public string GivenName { get; private set;  }
        
        /// <summary>
        /// The users middle name.
        /// </summary>
        public string MiddleName { get; private set; }
        
        public bool HasMiddleName { get; set; }
        
        /// <summary>
        /// The users last or family name.
        /// </summary>
        public string FamilyName { get; private set;  }
        
        /// <summary>
        /// The users or name they want to be called by. 
        /// </summary>
        public string Nickname { get; private set;  }
        
        /// <summary>
        /// The users date of birth.
        /// </summary>
        public DateTime DateOfBirth { get; }

        public Validations Errors => _validations;

        public bool IsValid()
        {
            return _validations.Count == 0;
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(MiddleName))
                {
                    return $"{GivenName} {FamilyName}";                    
                }
                return $"{GivenName} {MiddleName} {FamilyName}";
            }
        }
        
        public Validations ChangeName(string givenName, string familyName)
        {
            var validations = new Validations();
            validations.Add(ValidateGivenName(givenName).Errors);
            validations.Add(ValidateFamilyName(familyName).Errors);                        
            
            if (validations.Count != 0) return validations;
            
            GivenName = givenName;
            HasMiddleName = false;
            FamilyName = familyName;
            return validations;
        }

        public Validations ChangeName(string givenName, string middleName, string familyName)
        {
            var validations = new Validations();
            validations.Add(ChangeName(givenName, familyName).Errors);
            HasMiddleName = true;
            validations.Add(ValidateMiddleName(middleName).Errors);

            if (validations.Count != 0) return validations;
            MiddleName = middleName;
            HasMiddleName = true;
            return validations;
        }

        public Validations ChangeNickname(string nickname)
        {
            var validations = new Validations();
            validations.Add(ValidateNickname(nickname).Errors);
            if (validations.Count > 0) return validations;
            
            Nickname = nickname;
            return validations;
        }

        private Validations Validate()
        {
            var validations = new Validations();
            validations.Add(ValidateGivenName(GivenName).Errors);
            validations.Add(ValidateMiddleName(MiddleName).Errors);
            validations.Add(ValidateFamilyName(FamilyName).Errors);
            if (!string.IsNullOrEmpty(Nickname))  //nickname is optional.
            {
                validations.Add(ValidateNickname(Nickname).Errors);
            }
            

            if (DateOfBirth.Year < DateTime.Now.AddYears(-130).Year)
            {
                validations.AddError(ErrorCode.InvalidDate, "The DateOfBirth is invalid", nameof(DateOfBirth));
            }            

            return validations;
        }                

        private Validations ValidateGivenName(string name)
        {
            var validations = new Validations();
            if (string.IsNullOrEmpty(name))
            {
                validations.AddError(ErrorCode.ValueRequired, "A given name is required.",nameof(GivenName));
            }
            if (!string.IsNullOrEmpty(name) && !RegexConstants.NameRegex.IsMatch(name))
            {
                validations.AddError(ErrorCode.InvalidName, "The GivenName has invalid characters.", nameof(GivenName));
            }

            return validations;
        }

        private Validations ValidateMiddleName(string name)
        {
            var validations = new Validations();
            if (HasMiddleName && string.IsNullOrEmpty(name))
            {
                validations.AddError(ErrorCode.ValueRequired, "The middle name has no value", nameof(MiddleName));
            }
            if (HasMiddleName && !string.IsNullOrEmpty(name) && !RegexConstants.NameRegex.IsMatch(name))
            {
                validations.AddError(ErrorCode.InvalidName, "The MiddleName has invalid characters.", nameof(MiddleName));
            }

            return validations;
        }

        private Validations ValidateFamilyName(string name)
        {
            var validations = new Validations();
            if (string.IsNullOrEmpty(name))
            {
                validations.AddError(ErrorCode.ValueRequired,"A value is required.",nameof(FamilyName));
            }
            if (!string.IsNullOrEmpty(name) && !RegexConstants.NameRegex.IsMatch(name))
            {
                validations.AddError(ErrorCode.InvalidName, "The FamilyName has invalid characters.", nameof(FamilyName));
            }

            return validations;
        }

        private Validations ValidateNickname(string name)
        {
            var validations = new Validations();
            if (string.IsNullOrEmpty(name))
            {
                validations.AddError(ErrorCode.ValueRequired, "A value is required.", nameof(Nickname));
            }
            if (!string.IsNullOrEmpty(name) && !RegexConstants.NameRegex.IsMatch(name))
            {
                validations.AddError(ErrorCode.InvalidName, "The nickname is not valid", nameof(Nickname));
            }
            return validations;
        }
    }
}