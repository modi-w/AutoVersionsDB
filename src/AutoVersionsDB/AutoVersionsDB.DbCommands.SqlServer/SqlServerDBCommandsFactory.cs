using AutoVersionsDB.DbCommands.Contract;
using System.Data.SqlClient;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBCommandsFactory : IDBCommandsFactory
    {
        public DBType DBType
        {
            get
            {
                return new DBType("SqlServer", "Sql Server");
            }
        }




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


        public IDBCommands CreateDBCommands(DBConnectionInfo dbConnectionInfo)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateConnectionString(dbConnectionInfo), 0);
            SqlServerDBCommands sqlServerDBCommands = new SqlServerDBCommands(sqlServerConnection);

            return sqlServerDBCommands;
        }

        public IDBBackupRestoreCommands CreateBackupRestoreCommands(DBConnectionInfo dbConnectionInfo)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateAdminConnectionString(dbConnectionInfo), 0);
            SqlServerDBBackupRestoreCommands sqlServerDBBackupRestoreCommands = new SqlServerDBBackupRestoreCommands(sqlServerConnection);

            return sqlServerDBBackupRestoreCommands;
        }

        public IDBQueryStatus CreateDBQueryStatus(DBConnectionInfo dbConnectionInfo)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(CreateAdminConnectionString(dbConnectionInfo), 0);
            SqlServerDBQueryStatus sqlServerDBQueryStatus = new SqlServerDBQueryStatus(sqlServerConnection);

            return sqlServerDBQueryStatus;
        }


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
                connectionStringBuilder.DataSource = ".";
            }
            else
            {
                connectionStringBuilder.DataSource = dbConnectionInfo.Server;
            }



            if (string.IsNullOrWhiteSpace(dbConnectionInfo.Username))
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
