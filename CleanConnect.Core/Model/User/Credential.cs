using System;
using System.Security.Cryptography;
using System.Text;
using CleanConnect.Common.Model;
using CleanConnect.Common.Model.Errors;
using Konscious.Security.Cryptography;

namespace CleanConnect.Core.Model.User
{
    public class Credential: Base<long>, IValidator
    {
        private readonly int _numFailsAllowed = 3;
        
        /// <summary>
        /// Plain text password.
        /// </summary>
        private string _password;
        
        /// <summary>
        /// The number of validation errors that have been found.
        /// </summary>
        private Validations _validations = new Validations();

        /// <summary>
        /// Create a new credential
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="checkPassword"></param>
        public Credential(string username, string password, string checkPassword)
        {
            Username = username;
            _password = password;            
            ValidateUsername(Username);
            _validations.Add(SetPassword(password, checkPassword).Errors);            
        }

        /// <summary>
        /// Load an existing credential.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hashedPassword"></param>
        /// <param name="salt"></param>
        /// <param name="created"></param>
        /// <param name="modified"></param>
        /// <param name="fails"></param>
        public Credential(long id, string username, string hashedPassword, string salt, DateTimeOffset created, DateTimeOffset modified, int fails)
            :base(id,created, modified)
        {
            Username = username;
            HashedPassword = hashedPassword;
            Salt = salt;            
            Fails = fails;
        }
        
        /// <summary>
        /// The credential username.
        /// </summary>
        public string Username { get; }
        
        /// <summary>
        /// The hashed password.
        /// </summary>
        public string HashedPassword { get; private set;  }

        /// <summary>
        /// Salt for the hashed password.
        /// </summary>
        public string Salt { get; private set;  }
        
        /// <summary>
        /// Date and time the credential was created.
        /// </summary>
        public DateTimeOffset Created { get; }
        
        /// <summary>
        /// Date and time the credential was last modified.
        /// </summary>
        public DateTimeOffset Modified { get; private set; }
        
        /// <summary>
        /// The number of times the credential has failed validation since the last successful attempt.
        /// </summary>
        public int Fails { get; private set;  }
        
        /// <summary>
        /// The validation errors.
        /// </summary>
        public Validations Errors => _validations;
        
         /// <summary>
        /// Validate the password.
        /// TODO: add password hashing.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidatePassword(string password)
        {            
            return !IsLocked() && HashedPassword == HashPassword(password,Salt);
        }        

        /// <summary>
        /// The user has already authenticated and is changing their password.
        /// </summary>
        /// <param name="oldPassword"></param>
        /// <param name="password"></param>
        /// <param name="checkPassword"></param>
        /// <returns>The list of validation errors.  If no errors are returned the password was updated.</returns>
        public Validations ChangePassword(string oldPassword, string password, string checkPassword)
        {
            var validations = new Validations();
            if (!ValidatePassword(oldPassword))
            {                
                validations.AddError(ErrorCode.PasswordInvalid, "The supplied password does not meet the password rules.");
            }
            if (oldPassword == password)
            {
                validations.AddError(ErrorCode.PasswordInvalid, "The old password is the same as the new password.");
            }

            validations.Add(SetPassword(password, checkPassword).Errors);

            return validations;
        }

        public bool IsLocked()
        {
            return Fails >= _numFailsAllowed;            
        }

        /// <summary>
        /// Set the current password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="checkPassword"></param>
        private Validations SetPassword(string password, string checkPassword)
        {            
            var validations = new Validations();
            if (!RegexConstants.PasswordRegex.IsMatch(password))
            {
                _validations.AddError(ErrorCode.PasswordInvalid, "The supplied password does not meet the password rules.");
            }
            if (password != checkPassword)
            {
                _validations.AddError(ErrorCode.PasswordsDifferent, "You have not entered the same password.");
            }            
            if (validations.Count > 0) return validations;
            
            Salt = GenerateSalt();
            HashedPassword = HashPassword(password, Salt);
            return validations;

        }

        private string HashPassword(string password, string salt)
        {
            Argon2d hashAlgorithm = new Argon2d(Encoding.UTF8.GetBytes(password))
            {
                Salt = Encoding.UTF8.GetBytes(salt),
                Iterations = 5,
                MemorySize = 8192,
                DegreeOfParallelism = 2                
            };
            return Convert.ToBase64String(hashAlgorithm.GetBytes(512));            
        }

        private string GenerateSalt()
        {
            byte[] bytes = new byte[256];
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private void ValidateUsername(string username)
        {
            if (!RegexConstants.UsernameRegex.IsMatch(username))
            {
               _validations.AddError(ErrorCode.InvalidUsername, "The supplied username is not valid.");
            }
        }

        public bool IsValid()
        {
            return _validations.Count == 0;
        }

        
    }
}