using Autofac;

namespace CleanConnect.Web.Services
{
    /// <summary>
    /// Startup tasks for the server.
    /// </summary>
    public interface IStartupService
    {
        /// <summary>
        /// Load any configured clients.
        /// </summary>
        void LoadClients();
        
        /// <summary>
        /// Migrate the database if required.
        /// </summary>
        void MigrateDb();
    }
}