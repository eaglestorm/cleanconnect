using System;
using System.Collections.Generic;

namespace CleanConnect.Common.Model.Settings
{    
    /// <summary>
    /// Configure the clients to be used with the server.
    /// </summary>
    public class ClientSettings
    {
        /// <summary>
        /// Client id
        /// </summary>
        public Guid Id { get; set;  }
        
        /// <summary>
        /// Display name for the client.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Client secret or password
        /// </summary>
        public string Secret { get; set; }
        
        /// <summary>
        /// Allowed redirect uris for the client.
        /// </summary>
        public IList<string> RedirectUris { get; set;  }
    }
}