using Autofac;
using AutoMapper;
using CleanConnect.Core;
using CleanConnect.Web.Services;

namespace CleanConnect.Web
{
    public class WebModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartupService>().As<IStartupService>();
            builder.Register(m => new Mapper(CreateMapperConfig())).As<IMapper>();
        }

        private MapperConfiguration CreateMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreProfile>();
            });
        }
    }
}