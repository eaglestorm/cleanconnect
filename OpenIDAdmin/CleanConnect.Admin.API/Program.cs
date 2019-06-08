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

namespace CleanConnect.Admin.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()                
                .ConfigureServices(s=>s.AddAutofac())
                .UseUrls("http://localhost:6001")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, config) =>
                {
                    IHostingEnvironment env = context.HostingEnvironment;
                    config.AddJsonFile("appsettings.json",
                            false,
                            true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                            true);
                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }
    }
}