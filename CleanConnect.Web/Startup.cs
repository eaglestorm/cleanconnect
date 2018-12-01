using System;
using System.Collections.Generic;
using System.Linq;
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
using CleanConnect.Common.Model.Settings;
using CleanConnect.Db;
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
           
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {                       
            var dbSettings = Configuration.GetSection("DbSettings");
            //TODO: encrypt password.
            var store = DocumentStore.For(s =>
            {
                s.Connection($"User ID={dbSettings["Username"]};Password={dbSettings["PlainPassword"]};host={dbSettings["Host"]};Port={dbSettings["Port"]};Database={dbSettings["Database"]};Pooling=True;Enlist=True");                
                s.AutoCreateSchemaObjects = Environment.IsDevelopment() ? AutoCreate.All : AutoCreate.CreateOrUpdate;                    
            });

            builder.Register(c => store).SingleInstance();
            builder.RegisterType<IDocumentSession>().InstancePerLifetimeScope();
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new WebModule());
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
            startupService.MigrateDb();
            startupService.LoadClients();
        }
    }
}