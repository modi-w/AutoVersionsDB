using AutoVersionsDB.DbCommands.Contract;

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

   

        public IDBConnection CreateDBConnection(string connectionString, int timeout)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(connectionString, timeout);

            return sqlServerConnection;
        }


        public IDBCommands CreateDBCommands(string connectionString, int timeout)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(connectionString, timeout);
            SqlServerDBCommands sqlServerDBCommands = new SqlServerDBCommands(sqlServerConnection);

            return sqlServerDBCommands;
        }

        public IDBBackupRestoreCommands CreateBackupRestoreCommands(string connectionString, int timeout)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(connectionString, timeout);
            SqlServerDBBackupRestoreCommands sqlServerDBBackupRestoreCommands = new SqlServerDBBackupRestoreCommands(sqlServerConnection);

            return sqlServerDBBackupRestoreCommands;
        }

        public IDBQueryStatus CreateDBQueryStatus(string connectionString, int timeout)
        {
            SqlServerConnection sqlServerConnection = new SqlServerConnection(connectionString, timeout);
            SqlServerDBQueryStatus sqlServerDBQueryStatus = new SqlServerDBQueryStatus(sqlServerConnection);

            return sqlServerDBQueryStatus;
        }

    }
}
