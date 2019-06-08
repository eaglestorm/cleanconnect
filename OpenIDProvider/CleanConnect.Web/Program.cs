using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using CleanConnect.Web.Init;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CleanConnect.Web
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; }
        
        public async static Task Main(
            string[] args)
        {
            
            //write any exceptions on startup to the log.
            try
            {
                var host = CreateWebHostBuilder(args).Build();
                //run startup tasks.
                await host.RunTasksAsync();
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }
        
        private static IWebHostBuilder CreateWebHostBuilder(string[] args) {
            
            //integration tests don't call program.main
            var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{currentEnv}.json", optional: true)
                .AddJsonFile("Config/clients.json",false,true)
                .AddJsonFile($"Config/clients.{currentEnv}.json",true)
                .AddJsonFile("certificate.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"certificate.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            
            var certificateSettings = Configuration.GetSection("certificateSettings");
            string certificateFileName = certificateSettings.GetValue<string>("filename");
            string certificatePassword = certificateSettings.GetValue<string>("password");

            var certificate = new X509Certificate2(certificateFileName, certificatePassword);

            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
            
            return new WebHostBuilder()
                .UseKestrel(
                    options =>
                    {
                        options.AddServerHeader = false;
                        options.Listen(IPAddress.Loopback, 5005, listenOptions =>
                        {
                            listenOptions.UseHttps(certificate);
                        });
                    }
                    )
                .ConfigureServices(services => services.AddAutofac())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(x=> x.AddConfiguration(Configuration))
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseSerilog();
        }
       
    }
}