
namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBCommandsFactory
    {
        DBType DBType { get; }

        IDBConnection CreateDBConnection(DBConnectionInfo dbConnectionInfo);
        IDBConnection CreateAdminDBConnection(DBConnectionInfo dbConnectionInfo);

        IDBCommands CreateDBCommands(DBConnectionInfo dbConnectionInfo);

        IDBBackupRestoreCommands CreateBackupRestoreCommands(DBConnectionInfo dbConnectionInfo);

        IDBQueryStatus CreateDBQueryStatus(DBConnectionInfo dbConnectionInfo);
    }
}
