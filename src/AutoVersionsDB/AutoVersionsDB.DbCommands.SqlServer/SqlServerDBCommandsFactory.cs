using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.SqlServer.Utils;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBCommandsFactory : IDBCommandsFactory
    {
        public string DBTypeCode
        {
            get
            {
                return "SqlServer";
            }
        }
        public string DBTypeName
        {
            get
            {
                return "Sql Server";
            }
        }

        public IDBConnectionManager CreateDBConnectionManager(string connectionString, int timeout)
        {
            SqlServerConnectionManager sqlServerConnectionManager = new SqlServerConnectionManager(connectionString, timeout);

            return sqlServerConnectionManager;
        }


        public IDBCommands CreateDBCommands(string connectionString, int timeout)
        {
            SqlServerConnectionManager sqlServerConnectionManager = new SqlServerConnectionManager(connectionString, timeout);
            SqlServerDBCommands sqlServerDBCommands = new SqlServerDBCommands(sqlServerConnectionManager);

            return sqlServerDBCommands;
        }

        public IDBBackupRestoreCommands CreateBackupRestoreCommands(string connectionString, int timeout)
        {
            SqlServerConnectionManager sqlServerConnectionManager = new SqlServerConnectionManager(connectionString, timeout);
            SqlServerDBBackupRestoreCommands sqlServerDBBackupRestoreCommands = new SqlServerDBBackupRestoreCommands(sqlServerConnectionManager);

            return sqlServerDBBackupRestoreCommands;
        }

        public IDBQueryStatus CreateDBQueryStatus(string connectionString, int timeout)
        {
            SqlServerConnectionManager sqlServerConnectionManager = new SqlServerConnectionManager(connectionString, timeout);
            SqlServerDBQueryStatus sqlServerDBQueryStatus = new SqlServerDBQueryStatus(sqlServerConnectionManager);

            return sqlServerDBQueryStatus;
        }

    }
}
