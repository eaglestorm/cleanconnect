using System;
using System.Collections.Generic;
using Marten.Schema;

namespace CleanConnect.Core.Dal.Record
{
    /// <summary>
    /// The record of a client that gets persisted.
    /// </summary>
    public class ClientRecord
    {
        /// <summary>
        /// Client id
        /// </summary>
        public Guid Id { get; set;  }
        
        /// <summary>
        /// Client secret or password
        /// </summary>  
        public string Secret { get; set; }
        
        /// <summary>
        /// The name of the client.  For display mostly.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Allowed redirect uris for the client.
        /// </summary>
        public IList<string> RedirectUris { get; set;  }
    }
}