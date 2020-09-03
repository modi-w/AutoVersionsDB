
namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBCommandsFactory 
    {
        DBType DBType { get; }

        IDBConnection CreateDBConnection(string connectionString, int timeout);

        IDBCommands CreateDBCommands(string connectionString, int timeout);

        IDBBackupRestoreCommands CreateBackupRestoreCommands(string connectionString, int timeout);

        IDBQueryStatus CreateDBQueryStatus(string connectionString, int timeout);


        void ReleaseService(object serviceToRelease);
    }
}
