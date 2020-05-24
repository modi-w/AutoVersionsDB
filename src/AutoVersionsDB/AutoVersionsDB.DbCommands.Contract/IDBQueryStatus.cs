using System;

namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBQueryStatus: IDisposable
    {
        int GetNumOfOpenConnection(string dbName);

        double GetBackupProcessStatus();
        
        double GetRestoreProcessStatus();

    }
}
