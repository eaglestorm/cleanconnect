using CleanConnect.Common.Model.Identity;
using CleanConnect.Core.Model.Authorization;

namespace CleanConnect.Core.Model.Client
{
    /// <summary>
    /// Database access for the authorization request.
    /// </summary>
    public interface IAuthorizationRequestRepository
    {
        /// <summary>
        /// Save the authorisation request.
        /// </summary>
        /// <remarks>
        /// Implementations should check to see if the id exists and then either insert or update.
        /// </remarks>
        /// <param name="request"></param>
        void Save(AuthenticationRequest request);
        
        /// <summary>
        /// Get the authentication request specified by the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AuthenticationRequest Get(long id);
    }
}