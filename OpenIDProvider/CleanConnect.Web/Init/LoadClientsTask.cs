using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanConnect.Common.Model.Settings;
using CleanConnect.Core.Model.Client;
using Microsoft.Extensions.Options;

namespace CleanConnect.Web.Init
{
    /// <summary>
    /// Enable configuration of clients via a config file.
    /// </summary>
    public class LoadClientsTask: IStartupTask
    {
        private readonly ClientsConfig _clientsConfig;
        private readonly IClientRepository _clientRepository;
        
        public LoadClientsTask(IOptions<ClientsConfig> options, IClientRepository clientRepository)
        {
            _clientsConfig = options.Value;
            _clientRepository = clientRepository;

        }
        
        public Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var clients = _clientRepository.GetAll();
            foreach (var client in _clientsConfig.Clients)
            {
                if (clients.All(x => x.Id != client.Id))
                {
                    _clientRepository.Save(new Client(client.Id,client.Name,client.Secret,client.RedirectUris));
                }
            }
            return Task.CompletedTask;
        }
    }
}