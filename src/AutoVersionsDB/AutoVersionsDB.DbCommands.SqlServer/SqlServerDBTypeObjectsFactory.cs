using AutoVersionsDB.DbCommands.Contract;
using System.Data.SqlClient;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBTypeObjectsFactory : IDBTypeObjectsFactory
    {
        public DBType DBType => new DBType("SqlServer", "Sql Server");




        public IDBConnection CreateDBConnection(DBConnectionInfo dbConnectionInfo)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateConnectionString(dbConnectionInfo), 0);

            return sqlServerConnection;
        }

        public IDBConnection CreateAdminDBConnection(DBConnectionInfo dbConnectionInfo)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateAdminConnectionString(dbConnectionInfo), 0);

            return sqlServerConnection;
        }

        public IDBScriptsProvider CreateDBScriptsProvider()
        {
            return new SQLServerDBScriptsProvider();
        }



        //public DBCommands CreateDBCommands(DBConnectionInfo dbConnectionInfo)
        //{
        //    SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateConnectionString(dbConnectionInfo), 0);
        //    DBCommands sqlServerDBCommands = new DBCommands(sqlServerConnection);

        //    return sqlServerDBCommands;
        //}

        //public IDBBackupRestoreCommands CreateBackupRestoreCommands(DBConnectionInfo dbConnectionInfo)
        //{
        //    SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateAdminConnectionString(dbConnectionInfo), 0);
        //    DBBackupRestoreCommands sqlServerDBBackupRestoreCommands = new DBBackupRestoreCommands(sqlServerConnection);

        //    return sqlServerDBBackupRestoreCommands;
        //}

        //public DBQueryStatus CreateDBQueryStatus(DBConnectionInfo dbConnectionInfo)
        //{
        //    SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateAdminConnectionString(dbConnectionInfo), 0);
        //    DBQueryStatus sqlServerDBQueryStatus = new DBQueryStatus(sqlServerConnection);

        //    return sqlServerDBQueryStatus;
        //}


        private string CreateConnectionString(DBConnectionInfo dbConnectionInfo)
        {
            return CreateConnecntionStringByDBName(dbConnectionInfo, dbConnectionInfo.DBName);
        }


        private string CreateAdminConnectionString(DBConnectionInfo dbConnectionInfo)
        {
            return CreateConnecntionStringByDBName(dbConnectionInfo, "master");
        }


        private static string CreateConnecntionStringByDBName(DBConnectionInfo dbConnectionInfo, string dbName)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();

            connectionStringBuilder.InitialCatalog = dbName;

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
