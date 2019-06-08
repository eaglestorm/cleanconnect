using System;
using System.Collections.Generic;

namespace CleanConnect.Core.Model.Client
{
    /// <summary>
    /// Persitance storage interface for clients.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Get all the clients.
        /// </summary>
        /// <returns></returns>
        IList<Client> GetAll();
        
        /// <summary>
        /// Gets the client specified by the UID.
        /// </summary>
        /// <returns></returns>
        Client Get(Guid id);

        /// <summary>
        /// Inserts or updates the given client.
        /// Used when loading clients from the configuration.
        /// </summary>
        /// <param name="client"></param>
        void Save(Client client);
    }
}