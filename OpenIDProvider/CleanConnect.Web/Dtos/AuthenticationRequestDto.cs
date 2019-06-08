using System.ComponentModel.DataAnnotations;
using CleanConnect.Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace CleanConnect.Core.Messages
{
    public class AuthenticationRequestDto
    {
        /// <summary>
        /// The requested scopes
        /// </summary>
        [FromQuery]
        //[FromBody]
        [BindProperty(Name = "scope")]
        [Required]
        [RegularExpression(RegexConstants.NamePattern)]
        public string Scope { get; set;  }
        
        /// <summary>
        /// The requested response type.
        /// </summary>
        [FromQuery]
        //[FromBody]
        [BindProperty(Name = "response_type")]
        [Required]
        [RegularExpression(RegexConstants.NamePattern)]
        public string ResponseType { get; set; }
        
        /// <summary>
        /// The client
        /// </summary>
        [FromQuery]
        //[FromBody]
        [BindProperty(Name = "client_id")]
        [RegularExpression(RegexConstants.NamePattern)]
        public string ClientId { get; set; }
        
        /// <summary>
        /// The requested redirecct uri.
        /// </summary>
        [FromQuery]
        [BindProperty(Name = "redirect_uri")]
        //[FromBody]
        [Required]        
        [RegularExpression(RegexConstants.UriPattern)]
        public string RedirectUri { get; set; }
        
        /// <summary>
        /// The state client validation parameter.
        /// Used by the client application to verify the response. 
        /// </summary>
        [FromQuery]
        [BindProperty(Name = "state")]
        //[FromBody]
        [Required]
        [RegularExpression(RegexConstants.UuidPattern)]
        public string State { get; set; }
        
        /// <summary>
        /// Nonce provided with the request.
        /// </summary>
        [FromQuery]
        //[FromBody]
        [BindProperty(Name = "nonce")]
        [RegularExpression(RegexConstants.UuidPattern)]
        public string Nonce { get; set; }
        
        /// <summary>
        /// The requested way to display the authorization request.
        /// </summary>
        [FromQuery]
        [BindProperty(Name = "display")]
        //[FromBody]
        [RegularExpression(RegexConstants.NamePattern)]
        public string Display { get; set; }

        /// <summary>
        /// Sets the max age of a end users session.
        /// </summary>
        [FromQuery]
       // [FromBody]
        [BindProperty(Name = "max_age")]
        public int MaxAge { get; set;  }
        
        /// <summary>
        /// The requested locales to use when authenticating the end user.
        /// </summary>
        [FromQuery]
        [BindProperty(Name = "ui_locales")]
        //[FromBody]
        [RegularExpression(RegexConstants.NamePattern)]
        public string Locales { get; set;  }
        
        /// <summary>
        /// Previous id token issued by this server.
        /// </summary>
        [FromQuery]
        [BindProperty(Name = "id_token_hint")]
        //[FromBody]
        [RegularExpression(RegexConstants.NamePattern)]
        public string TokenHint { get; set; }
        
        /// <summary>
        /// Login hint to the end users identity
        /// </summary>
        /// <remarks>
        /// Not yet supported.
        /// </remarks>
        [FromQuery]
        [BindProperty(Name = "login_hint")]
        //[FromBody]
        [RegularExpression(RegexConstants.NamePattern)]
        public string LoginHint { get; set; }
        
        /// <summary>
        /// Acr values requested.
        /// </summary>
        /// <remarks>
        /// Not yet supported.
        /// </remarks>
        [FromQuery]
        [BindProperty(Name = "acr_values")]
        //[FromBody]
        [RegularExpression(RegexConstants.NamePattern)]
        public string AcrValues { get; set; }
        
        /// <summary>
        /// Locales for returned claims
        /// </summary>
        /// <remarks>
        /// Not yet supported.
        /// </remarks>
        [FromQuery]
        [BindProperty(Name = "claims_locales")]
        //[FromBody]
        [RegularExpression(RegexConstants.NamePattern)]
        public string ClaimsLocales { get; set; }

        /// <summary>
        /// The requested prompts.
        /// </summary>
        [FromQuery(Name = "prompt")]
        [BindProperty(Name = "prompt")]
        //[FromBody]
        [RegularExpression(RegexConstants.NamePattern)]
        public string Prompts { get; set;  }
    }
}