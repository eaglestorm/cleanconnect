using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace CleanConnect.Core.Messages
{
    public class AuthenticationResponseDto
    {
        public AuthenticationResponseDto(string code, string state)
        {
            Code = code;
            State = state;
        }

        public AuthenticationResponseDto(string error)
        {
            Error = error;
        }
        
        public string Code { get; }
        
        public string State { get; }
        
        public string Error { get; }

        public QueryString GetQueryString()
        {
            var queryBuilder = new QueryBuilder();
            if (string.IsNullOrEmpty(Error))
            {
                queryBuilder.Add("code",Code);
                queryBuilder.Add("state", State);                
            }
            else
            {
                queryBuilder.Add("error",Error);
            }

            return queryBuilder.ToQueryString();
        }
    }
}