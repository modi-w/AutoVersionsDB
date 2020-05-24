using System;


namespace AutoVersionsDB.DbCommands.Contract
{
    //Comment: we seperate this from IDBCommands, because here we use "Master" DB instead of the specific DB.
    public interface IDBBackupRestoreCommands : IDisposable
    {
        void CreateDbBackup(string filename, string dbName);

        void RestoreDbFromBackup(string filename, string dbName);

    }
}
