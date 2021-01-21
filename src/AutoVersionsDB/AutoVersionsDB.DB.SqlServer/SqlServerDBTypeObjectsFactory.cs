using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.Helpers;
using System.Data.SqlClient;

namespace AutoVersionsDB.DB.SqlServer
{
    public class SqlServerDBTypeObjectsFactory : IDBTypeObjectsFactory
    {
        public const string DBTypeCode = "SqlServer";

        public DBType DBType => new DBType(DBTypeCode, "Sql Server");




        public IDBConnection CreateDBConnection(DBConnectionInfo dbConnectionInfo)
        {
            dbConnectionInfo.ThrowIfNull(nameof(dbConnectionInfo));

            SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateConnectionString(dbConnectionInfo), dbConnectionInfo.Timeout);

            return sqlServerConnection;
        }

        public IDBConnection CreateAdminDBConnection(DBConnectionInfo dbConnectionInfo)
        {
            dbConnectionInfo.ThrowIfNull(nameof(dbConnectionInfo));

            SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateAdminConnectionString(dbConnectionInfo), dbConnectionInfo.Timeout);

            return sqlServerConnection;
        }

        public IDBScriptsProvider CreateDBScriptsProvider()
        {
            return new SQLServerDBScriptsProvider();
        }






        private static string CreateConnectionString(DBConnectionInfo dbConnectionInfo)
        {
            return CreateConnecntionStringByDBName(dbConnectionInfo, dbConnectionInfo.DBName);
        }


        private static string CreateAdminConnectionString(DBConnectionInfo dbConnectionInfo)
        {
            return CreateConnecntionStringByDBName(dbConnectionInfo, "master");
        }


        private static string CreateConnecntionStringByDBName(DBConnectionInfo dbConnectionInfo, string dbName)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                InitialCatalog = dbName
            };

            if (string.IsNullOrWhiteSpace(dbConnectionInfo.Server))
            {
                connectionStringBuilder.DataSource = "(local)";
            }
            else
            {
                connectionStringBuilder.DataSource = dbConnectionInfo.Server;
            }



            if (!string.IsNullOrWhiteSpace(dbConnectionInfo.Username))
            {
                connectionStringBuilder.UserID = dbConnectionInfo.Username;
                connectionStringBuilder.Password = dbConnectionInfo.Password;
            }
            else
            {
                connectionStringBuilder.IntegratedSecurity = true;
            }


            return connectionStringBuilder.ConnectionString;
        }


    }
}
