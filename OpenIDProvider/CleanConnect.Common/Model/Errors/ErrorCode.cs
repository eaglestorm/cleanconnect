using System;
using Microsoft.VisualBasic.CompilerServices;

namespace CleanConnect.Common.Model.Errors
{
    /// <summary>
    /// List of error codes for excpetions.
    /// </summary>
    /// <remarks>
    /// Contains two different classes of errors.
    /// https://martinfowler.com/articles/replaceThrowWithNotification.html
    ///
    /// Note:  the errors indicated here are more than allowed by the spec so most of these will only be logged. 
    /// </remarks>
    public sealed class ErrorCode
    {        
        private readonly ErrorGroup _group;
        private int _number;

        /// Errors that should be thrown as exceptions
        #region Exceptions 

        public static readonly ErrorCode InvalidClient = new ErrorCode(ErrorGroup.Configuration, 1, AsciiError.UnauthorizedClient, "The supplied client is not configured correctly.");

        #endregion

        /// Errors that should not throw exceptions but be returned to the client as a validation response 400.
        #region ASCII Validations
        
        public static readonly ErrorCode Required = new ErrorCode(ErrorGroup.Validation, 1, AsciiError.InvalidRequest, "A required value is missing.");

        /// <summary>
        /// The display value for the authorization request is not valid.
        /// </summary>
        public static readonly ErrorCode InvalidDisplay = new ErrorCode(ErrorGroup.Validation, 101, AsciiError.InvalidRequest, "The supplied display value is not supported or not valid.");
        
        /// <summary>
        /// One of the prompt values for the authorization request is not valid.
        /// </summary>
        public static readonly ErrorCode InvalidPrompt = new ErrorCode(ErrorGroup.Validation,102, AsciiError.InvalidRequest, "The supplied display value is not supported or not valid.");
        
        /// <summary>
        /// The client has supplied one or more locale values that are not recognized.
        /// </summary>
        public static readonly ErrorCode InvalidLocale = new ErrorCode(ErrorGroup.Validation,103, AsciiError.InvalidRequest, "The supplied locale is not supported or not valid.");
        
        /// <summary>
        /// Indicates that the Authentication request did not provide the open id scope.
        /// </summary>
        public static readonly ErrorCode InvalidScope = new ErrorCode(ErrorGroup.Validation,104, AsciiError.InvalidScope, "The openid scope must be included in the request.");
        
        /// <summary>
        /// Indicates that the none prompt should be specified by itself and this has not been done.
        /// </summary>
        public static readonly ErrorCode InvalidNonePrompt = new ErrorCode(ErrorGroup.Validation,105, AsciiError.InvalidRequest, "The none prompt must be included in the request.");
        
        /// <summary>
        ///  The redirect uri supplied with the request is invalid. 
        /// </summary>
        public static readonly ErrorCode InvalidRedirectUri = new ErrorCode(ErrorGroup.Validation,106, AsciiError.InvalidRequest, "The redrect uri is not valid for the client.");
        
        /// <summary>
        /// Your access has been denied.
        /// </summary>
        public static readonly ErrorCode AccessDenied = new ErrorCode(ErrorGroup.Validation, 107, AsciiError.AccessDenied, "Access Denied");
        
        
        public static readonly ErrorCode InvalidRequest = new ErrorCode(ErrorGroup.Validation, 108, AsciiError.InvalidRequest, "Invalid request");
        
        public static readonly ErrorCode ServerError = new ErrorCode(ErrorGroup.Validation, 109,AsciiError.ServerError,"The server has encountered an unexpected error.");
        

        #endregion

        //Errors not returned to the RP.
        #region Authorisation Server Errors

        public static readonly  ErrorCode InvalidMethodCall = new ErrorCode(ErrorGroup.General, 1, "This method call is not valid.");
        
        /// <summary>
        /// A users password was attempted to be changed but the new password did not match the repeated password field.
        /// </summary>
        public static readonly ErrorCode PasswordsDifferent = new ErrorCode(ErrorGroup.Validation, 201, "The supplied passwords did not match.");
        
        /// <summary>
        /// A user tried to change their password but the new password did not meet the password rules. 
        /// </summary>
        public static readonly ErrorCode PasswordInvalid = new ErrorCode(ErrorGroup.Validation, 202, "The supplied password does not meet password rules.");
        
        public static readonly ErrorCode InvalidUsername = new ErrorCode(ErrorGroup.Validation, 203, "The supplied username is not valid.");
        
        public static readonly ErrorCode InvalidName = new ErrorCode(ErrorGroup.Validation, 204, "A string contains invalid data.");
        
        public static readonly ErrorCode InvalidDate = new ErrorCode(ErrorGroup.Validation, 204, "A date is not valid.");
        
        public static readonly ErrorCode InvalidEmail = new ErrorCode(ErrorGroup.Validation, 204, "The email address is not valid.");
        
        public static readonly ErrorCode InvalidPhone = new ErrorCode(ErrorGroup.Validation, 204, "The phone number is not valid.");
        
        public static readonly ErrorCode ValueRequired = new ErrorCode(ErrorGroup.Validation, 205, "The phone number is not valid.");
        
        public static readonly ErrorCode UserLocked = new ErrorCode(ErrorGroup.Validation, 206,"The user is currently locked.");

        #endregion


        /// <summary>
        /// Create a new error code.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="number"></param>
        /// <param name="asciiCode"></param>
        /// <param name="message"></param>
        public ErrorCode(
            ErrorGroup @group,
            int number,
            AsciiError asciiCode,
            string message)
        {
            AsciiCode = asciiCode;
            Message = message;
            _group = group;
            _number = number;
        }
        
        /// <summary>
        /// Errors that are not required by the open id spec and hence not returned to the RP, i.e. password validation errors.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="number"></param>
        /// <param name="message"></param>
        public ErrorCode(
            ErrorGroup @group,
            int number,
            string message)
        {
            Message = message;
            _group = group;
            _number = number;
        }

        /// <summary>
        /// Standards compliant error code defined in the spec.
        /// </summary>
        public AsciiError AsciiCode { get; }

        public string Code => $"{_group.Code}{_number | 0000} ";

        /// <summary>
        /// Readable message for the error.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// External description for the error as per the spec.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return AsciiCode.ToString();
        }

        /// <summary>
        /// Internal error description for the logs, etc.
        /// </summary>
        /// <returns></returns>
        public string GetInternalDescription()
        {
            return $"Error Code: {Code}, Ascii: {AsciiCode}  Description: {Message}";
        }
    }
}