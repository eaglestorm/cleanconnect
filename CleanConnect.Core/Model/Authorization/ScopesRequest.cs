using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Model.Authorization
{
    public class ScopesRequest: IValidator
    {
        private readonly IList<string> _scopes = new List<string>();
        
        private Validations _validations = new Validations();               
        
        public ScopesRequest(string scopes)
        {
            var list = (scopes.Split(' '));
            foreach (var scope in list)
            {
                _scopes.Add(scope);
            }

            if (!_scopes.Contains("openid"))
            {
                _validations.AddError(ErrorCode.InvalidScope, "The open id scope is required.");
            }
        }

        public int Count => _scopes.Count;

        public bool Contains(string scope)
        {
            return _scopes.Contains(scope);
        }


        public bool IsValid()
        {
            return _validations.Count == 0;
        }

        public override string ToString()
        {
            return _scopes.Aggregate("", (current, str) => current + (str + " ")).Trim();
        }

        public Validations Errors => _validations;
    }
}