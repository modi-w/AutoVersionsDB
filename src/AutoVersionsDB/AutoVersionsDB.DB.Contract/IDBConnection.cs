using System;
using System.Data;

namespace AutoVersionsDB.DB.Contract
{
    public interface IDBConnection : IDisposable
    {
        bool IsDisposed { get; }

        void Open();

        void Close();


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1021:Avoid out parameters", Justification = "<Pending>")]
        bool CheckConnection(out string outErrorMseeage);

        string DataBaseName { get; }

        string ConnectionString { get; }

        void ExecSQLCommandStr(string commandStr);

        DataTable GetSelectCommand(string sqlSelectCmd, int overrideTimeout = 0);

        int UpdateDataTableWithUpdateIdentityOnInsert(DataTable dataTable);
    }
}
