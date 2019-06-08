using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CleanConnect.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using CleanConnect.Common.Model.Settings;
using CleanConnect.Db;
using CleanConnect.Infrastructure;
using CleanConnect.Web.Init;
using CleanConnect.Web.Services;
using DbUp;
using Marten;
using Microsoft.Extensions.Options;

namespace CleanConnect.Web
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            services.Configure<CleanSettings>(Configuration);
            services.Configure<ClientsConfig>(Configuration);
            
            services.AddAutoMapper(typeof(InfrastructureProfile), typeof(WebProfile));

            services.AddMvc(
                options =>
                {
                    options.SslPort = 5005;
                    options.Filters.Add(new RequireHttpsAttribute());
                }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddAntiforgery(
                options =>
                {
                    options.Cookie.Name = "_af";
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.HeaderName = "X-XSRF-TOKEN";
                }
            );
            
            if (Environment.IsDevelopment())
            {
                //disable certificate errors.
                services.AddHttpClient("HttpClientWithSSLUntrusted").ConfigurePrimaryHttpMessageHandler(() =>
                    new HttpClientHandler
                    {
                        ClientCertificateOptions = ClientCertificateOption.Manual,
                        ServerCertificateCustomValidationCallback =
                            (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
                    });
            }
           
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {                       
            var dbSettings = Configuration.GetSection("DbSettings");
            //TODO: encrypt password.
            var store = DocumentStore.For(s =>
            {
                s.Connection($"User ID={dbSettings["Username"]};Password={dbSettings["PlainPassword"]};host={dbSettings["Host"]};Port={dbSettings["Port"]};database={dbSettings["Database"]};Pooling=True;Enlist=True");                
                s.AutoCreateSchemaObjects = Environment.IsDevelopment() ? AutoCreate.All : AutoCreate.CreateOrUpdate;
                
            });

            builder.Register(c => store).SingleInstance();
            builder.RegisterType<IDocumentSession>().InstancePerLifetimeScope();
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new WebModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterType<DbMigrationTask>().As<IStartupTask>();
            builder.RegisterType<LoadClientsTask>().As<IStartupTask>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            IStartupService startupService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }            

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });   
            //TODO: refactor this to use init services.
            //startupService.MigrateDb();
            startupService.LoadClients();
        }
    }
}