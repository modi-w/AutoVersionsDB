using System;


namespace AutoVersionsDB.DbCommands.Contract
{
    //Comment: we seperate this from IDBCommands, because here we use "Master" DB instead of the specific DB.
    public interface IDBBackupRestoreCommands //: IDisposable
    {
        void CreateDBBackup(string filename, string dbName);

        void RestoreDBFromBackup(string filename, string dbName, string dbFilesBasePath = null);

    }
}
