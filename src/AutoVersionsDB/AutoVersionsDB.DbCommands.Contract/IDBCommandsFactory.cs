
namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBCommandsFactory
    {
        DBType DBType { get; }

        IDBConnectionManager CreateDBConnectionManager(string connectionString, int timeout);

        IDBCommands CreateDBCommands(string connectionString, int timeout);

        IDBBackupRestoreCommands CreateBackupRestoreCommands(string connectionString, int timeout);

        IDBQueryStatus CreateDBQueryStatus(string connectionString, int timeout);
    }
}
