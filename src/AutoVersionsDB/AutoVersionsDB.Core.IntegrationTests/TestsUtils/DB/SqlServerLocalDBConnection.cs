using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using MartinCostello.SqlLocalDb;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB
{
    public class SqlServerLocalDBConnection
    {
        private static string _baseConnStr = null;
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

        private static SqlConnectionStringBuilder _connectionStringBuilder = null;
        public static SqlConnectionStringBuilder ConnectionStringBuilder
        {
            get
            {
                if (_connectionStringBuilder == null)
                {
                    _connectionStringBuilder = new SqlConnectionStringBuilder(BaseConnStr);
                }

                return _connectionStringBuilder;
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
