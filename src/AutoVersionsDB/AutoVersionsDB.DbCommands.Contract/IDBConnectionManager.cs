using System;

namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBConnectionManager : IDisposable
    {
        bool IsDisposed { get; }

        void Open();

        void Close();

        bool CheckConnection(out string outErrorMseeage);

        string DataBaseName { get; }


    }
}
