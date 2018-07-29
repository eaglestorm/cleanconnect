using System;
using System.Collections.Generic;
using CleanConnect.Common.Model;
using CleanConnect.Common.Model.Errors;

namespace CleanConnect.Core.Model
{
    public class Client
    {        
        public Client(
            Guid id,
            string secret,
            IList<string> redirectUris)
        {
            Id = id;
            Secret = secret;
            RedirectUris = redirectUris;
            IsValid();
        }
        
        /// <summary>
        /// Client id
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Client secret or password
        /// </summary>
        public string Secret { get; }
        
        /// <summary>
        /// Allowed redirect uris for the client.
        /// </summary>
        public IList<string> RedirectUris { get; }

        /// <summary>
        /// Are we allowed to redirect to the uri supplied in the authorization request.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public bool IsValidRedirectUri(
            string uri)
        {
            return RedirectUris.Contains(uri);
        }

        /// <summary>
        /// Ensure everything is configured correctly. 
        /// </summary>
        private void IsValid()
        {
            if (Id == Guid.Empty || string.IsNullOrEmpty(Secret) || RedirectUris == null || RedirectUris.Count <= 0)
            {
                throw new CleanConnectException(ErrorCode.InvalidClient,"The client is not configured correctly.");
            }
        }
    }
}