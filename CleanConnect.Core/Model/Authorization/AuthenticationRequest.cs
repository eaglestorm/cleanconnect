using System;
using System.Collections.Generic;
using System.Globalization;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Model.Authorization
{
    /// <summary>
    /// A request by a client to authenticate a user.
    /// </summary>
    /// <remarks>
    /// All properties not passed into the constructor are optional for the client to pass.
    /// </remarks>
    public class AuthenticationRequest: IValidator
    {
        private Validations _validations = new Validations();


        /// <summary>
        /// Constructor with all the required parameters.
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="responseType"></param>
        /// <param name="client"></param>
        /// <param name="redirectUri">requested redirect uri</param>
        /// <param name="state"></param>
        public AuthenticationRequest(string scopes, ResponseType responseType,Client.Client client, string redirectUri, string state)
        {
            Scopes = new ScopesRequest(scopes);
            if (!Scopes.IsValid())
            {
                Errors.Add(Scopes.Errors.Errors);
            }
            ResponseType = responseType;
            Client = client;
            RedirectUri = redirectUri;
            if (!Client.IsValidRedirectUri(redirectUri))
            {
                _validations.AddError(ErrorCode.InvalidRedirectUri, "The supplied redirect uri is invalid.");
            }
            State = state;           
            Locales = new List<CultureInfo>();            
        }
        
        
        /// <summary>
        /// Constructor with all the parameters.  Mostly used when loading the request from storage.
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="responseType"></param>
        /// <param name="client"></param>
        /// <param name="redirectUri"></param>
        /// <param name="state"></param>
        /// <param name="nonce"></param>
        /// <param name="display"></param>
        /// <param name="prompt"></param>
        /// <param name="maxAge"></param>
        /// <param name="locales"></param>
        /// <param name="tokenHint"></param>
        public AuthenticationRequest(string scopes, ResponseType responseType,Client.Client client, string redirectUri, string state, string nonce, Display display,
            Prompts prompts, DateTimeOffset maxAge, IList<CultureInfo> locales, string tokenHint)
        :this(scopes,responseType,client,redirectUri,state)
        {
            Nonce = nonce;
            Display = display;
            MaxAge = maxAge;
            Locales = locales;
            TokenHint = tokenHint;
            Prompts = Prompts;
        }
        
        /// <summary>
        /// The requested scopes
        /// </summary>
        public ScopesRequest Scopes { get; }
        
        /// <summary>
        /// The requested response type.
        /// </summary>
        public ResponseType ResponseType { get; }
        
        /// <summary>
        /// The client
        /// </summary>
        public Client.Client Client { get; }
        
        /// <summary>
        /// The requested redirecct uri.
        /// </summary>
        public string RedirectUri { get; }
        
        /// <summary>
        /// The state client validation parameter.
        /// Used by the client application to verify the response. 
        /// </summary>
        public string State { get; }
        
        /// <summary>
        /// Nonce provided with the request.
        /// </summary>
        public string Nonce { get; set; }
        
        /// <summary>
        /// The requested way to display the authorization request.
        /// </summary>
        public Display Display { get; private set; }

        /// <summary>
        /// Sets the max age of a end users session.
        /// </summary>
        public DateTimeOffset MaxAge { get; set;  }
        
        /// <summary>
        /// The requested locales to use when authenticating the end user.
        /// </summary>
        public IList<CultureInfo> Locales { get; private set;  }
        
        //public string Locales { get; set;  }
        public string TokenHint { get; set; }

        /// <summary>
        /// The requested prompts.
        /// </summary>
        public Prompts Prompts { get; private set;  }

        /// <summary>
        /// Set the prompts for the request.
        /// </summary>
        /// <param name="prompts"></param>
        public void SetPrompts(string prompts)
        {
            Prompts = new Prompts(prompts);
            if (!Prompts.IsValid())
            {
                Errors.Add(Prompts.Errors.Errors);
            }            
        }                
        
        /// <summary>
        /// Set the display given the string.
        /// </summary>
        /// <param name="display"></param>
        public void SetDisplay(string display)
        {

            if (Display.Page.ToString() == display)
            {
                Display = Display.Page;
            }
            else if(Display.Popup.ToString() == display)
            {
                Display = Display.Popup;
            }
            else if (Display.Touch.ToString() == display)
            {
                Display = Display.Touch;
            }
            else if (Display.Wap.ToString() == display)
            {
                Display = Display.Wap;
            }
            else
            {
                _validations.AddError(ErrorCode.InvalidDisplay, $"{display} is not a valid value for the display parameter.");
            }
        }

        /// <summary>
        /// set when the end user must be authenticated by.
        /// </summary>
        /// <param name="maxAge"></param>
        public void SetMaxAge(int maxAge)
        {
            MaxAge = DateTimeOffset.UtcNow.AddSeconds(maxAge);
        }

        public void SetLocales(string locales)
        {
            var locs = locales.Split(' ');
            foreach (var loc in locs)
            {
                try
                {
                    Locales.Add(new CultureInfo(loc));
                }
                catch (CultureNotFoundException ex)
                {
                    //TODO: logging via a service.
                    _validations.AddError(ErrorCode.InvalidLocale, $"{loc} is not recognized as a valid locale.");
                }
                
            }
        }        

        /// <summary>
        /// returns true if the request allows the end user to be authenticated.
        /// If false there will be one or more validaiton errors.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {            
            return Errors.Count == 0;
        }

        public Validations Errors => _validations;

        public bool LoginRequired()
        {
            return Prompts.CanShowLoginAndConsent();
        }
    }
}