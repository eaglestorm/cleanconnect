using System;

namespace CleanConnect.Infrastructure.Record
{
    public class AuthorizationRequestRecord
    {
        
        public long Id { get; set; }
        
        public string Scopes { get; set; }
        
        public int ResponseType { get; set; }
        
        public string RedirectUri { get; set;}
        
        public string State { get; set;}
        
        public string Nonce { get; set; }
        
        public int Display { get; set; }
        
        public DateTimeOffset MaxAge { get; set;  }
        
        public string Locales { get; set; }
        
        public string TokenHint { get; set; }
        
        public string Prompts { get; set; }
    }
}