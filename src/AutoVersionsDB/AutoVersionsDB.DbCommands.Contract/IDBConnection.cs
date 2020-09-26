using System;

namespace AutoVersionsDB.DbCommands.Contract
{
    public interface IDBConnection //: IDisposable
    {
        bool IsDisposed { get; }

        void Open();

        void Close();

        bool CheckConnection(out string outErrorMseeage);

        string DataBaseName { get; }

        string ConnectionString { get; }


    }
}
