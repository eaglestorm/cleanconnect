using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CleanConnect.Web
{
    public class Program
    {
        public static void Main(
            string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(x=>x.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, config) =>
                {
                    IHostingEnvironment env = context.HostingEnvironment;
                    config.AddJsonFile("appsettings.json",
                            false,
                            true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                            true)
                        .AddJsonFile("Config/clients.json",false,true)
                        .AddJsonFile($"Config/clients.{env.EnvironmentName}.json",true);
                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>()                
                .Build();
            
                host.Run();
        }
       
    }
}