using MartinCostello.SqlLocalDb;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.Helpers
{
    public class SqlServerInstanceHelpers
    {
        private static string _baseConnStr= null;
        public static string BaseConnStr
        {
            get
            {
                if (_baseConnStr == null)
                {
                    _baseConnStr = initSqlLocalDB();
                }

                return _baseConnStr;
            }
        }

        private static string initSqlLocalDB()
        {
            SqlLocalDbOptions sqlLocalDbOptions = new SqlLocalDbOptions()
            {
                NativeApiOverrideVersion = "14.0"
            };


            var services = new ServiceCollection().AddLogging((p) => p.AddConsole().SetMinimumLevel(LogLevel.Debug));
            var loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
            ISqlLocalDbApi localDB = new SqlLocalDbApi(sqlLocalDbOptions, loggerFactory);
            ISqlLocalDbInstanceInfo instance = localDB.GetOrCreateInstance(@"localtestdb");

            ISqlLocalDbInstanceManager manager = instance.Manage();

            if (!instance.IsRunning)
            {
                manager.Start();
            }

            return instance.GetConnectionString();
        }
    }
}
