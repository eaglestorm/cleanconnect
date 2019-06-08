using Autofac;
using CleanConnect.Common.Model.Settings;
using CleanConnect.Core.Model.Client;
using CleanConnect.Core.UseCase;
using CleanConnect.Db;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace CleanConnect.Web.Services
{
    public class StartupService: IStartupService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IHostingEnvironment _environment;
        private readonly CleanSettings _settings;
        private readonly ClientsConfig _clientsConfig;        
        
        public StartupService(IOptions<ClientsConfig> options, IClientRepository clientRepository, IOptions<CleanSettings> settings, IHostingEnvironment environment)
        {
            _clientRepository = clientRepository;
            _environment = environment;
            _settings = settings.Value;
            _clientsConfig = options.Value;
        }
        
        public void LoadClients()
        {
            foreach (var client in _clientsConfig.Clients)
            {
                _clientRepository.Save(new Client(client.Id,client.Name,client.Secret,client.RedirectUris));
            }
        }

        public void MigrateDb()
        {
            DbUpgrader.Upgrade(_settings.DbSettings,_environment);
        }
    }
}