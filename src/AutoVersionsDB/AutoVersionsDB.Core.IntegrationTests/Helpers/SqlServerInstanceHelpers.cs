using MartinCostello.SqlLocalDb;
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

            ISqlLocalDbApi localDB = new SqlLocalDbApi();
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
