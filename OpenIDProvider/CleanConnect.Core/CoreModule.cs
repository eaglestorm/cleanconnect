using Autofac;
using CleanConnect.Common.Contracts;
using CleanConnect.Common.Model.Settings;
using CleanConnect.Core.Messages;
using CleanConnect.Core.Model.Client;
using CleanConnect.Core.UseCase;
using CleanConnect.Core.UseCase.Impl;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CleanConnect.Core
{
    /// <summary>
    /// Core module
    /// </summary>
    public class CoreModule: Module
    {               
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthorizationRequestUseCase>().As<IUseCase<ProcessRequestMessage, ProcessRequestResponseMessage>>();
        }
    }
}