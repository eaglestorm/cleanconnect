using Autofac;
using CleanConnect.Core.Dal;
using CleanConnect.Core.Model.Client;

namespace CleanConnect.Infrastructure
{
    /// <summary>
    /// Autofac configuration for Infrastructure package.
    /// </summary>
    public class InfrastructureModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            //repositories
            builder.RegisterType<ClientRepository>().As<IClientRepository>();
        }
    }
}