
namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBCommandsFactory
    {
        string DBTypeCode { get; }
        string DBTypeName { get; }


        IDBConnectionManager CreateDBConnectionManager(string connectionString, int timeout);

        IDBCommands CreateDBCommands(string connectionString, int timeout);

        IDBBackupRestoreCommands CreateBackupRestoreCommands(string connectionString, int timeout);

        IDBQueryStatus CreateDBQueryStatus(string connectionString, int timeout);
    }
}
