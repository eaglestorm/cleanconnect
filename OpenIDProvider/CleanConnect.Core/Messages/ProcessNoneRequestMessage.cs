using System;
using CleanConnect.Common.Contracts;

namespace CleanConnect.Core.Messages
{
    /// <summary>
    /// Request to see if the user is already authenticated.
    /// </summary>
    public class ProcessNoneRequestMessage: IRequest<ProcessNoneResponseMessage>
    {
        public string Scope { get; set;  }
        
       
        public string ResponseType { get; set; }
        
       
        public Guid ClientId { get; set; }
        
        
        public string RedirectUri { get; set; }
        
        
        public string State { get; set; }
        
        
        public string Nonce { get; set; }
        
        
        public string Display { get; set; }

        
        public int MaxAge { get; set;  }
        
       
        public string Locales { get; set;  }
        
       
        public string TokenHint { get; set; }
        
        
        public string LoginHint { get; set; }
        
       
        public string AcrValues { get; set; }
        
       
        public string ClaimsLocales { get; set; }

        /// <summary>
        /// Should be none.
        /// </summary>
        public string Prompts { get; set;  }
    }
}