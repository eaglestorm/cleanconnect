using System.Reflection;
using CleanConnect.Common.Model.Settings;
using DbUp;
using DbUp.Engine.Filters;
using Microsoft.AspNetCore.Hosting;

namespace CleanConnect.Db
{
    public class DbUpgrader
    {
        public static void Upgrade(DbSettings dbSettings, IHostingEnvironment env)
        {
            var updateder = DeployChanges.To.SqlDatabase(dbSettings.GetConnectionString())
                .WithScriptsEmbeddedInAssembly(Assembly.GetAssembly(typeof(DbUpgrader)),x=>x.Contains(".Scripts."))
                .LogToAutodetectedLog()
                .Build();
            if (updateder.IsUpgradeRequired())
            {
                updateder.PerformUpgrade();
            }

            if (env.IsDevelopment())
            {
                var updateder = DeployChanges.To.SqlDatabase(dbSettings.GetConnectionString())
                    .WithScriptsEmbeddedInAssembly(Assembly.GetAssembly(typeof(DbUpgrader)),x=>x.Contains(".Sample."))
                    .LogToAutodetectedLog()
                    .Build();
                if (updateder.IsUpgradeRequired())
                {
                    updateder.PerformUpgrade();
                }
            }
        }
    }
}