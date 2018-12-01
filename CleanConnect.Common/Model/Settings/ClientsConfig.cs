using System.Collections.Generic;

namespace CleanConnect.Common.Model.Settings
{
    /// <summary>
    /// Configuration for clients.
    /// </summary>
    public class ClientsConfig
    {
        /// <summary>
        /// The list of valid clients.
        /// </summary>
        public IList<ClientSettings> Clients { get; set; }
    }
}