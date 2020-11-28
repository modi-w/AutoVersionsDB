using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Data;
using System.Globalization;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBQueryStatus : IDBQueryStatus, IDisposable
    {
        private readonly SqlServerConnection _sqlServerConnection;
        private readonly IDBScriptsProvider _dBScriptsProvider;



        public SqlServerDBQueryStatus(SqlServerConnection sqlServerConnection,
                                        IDBScriptsProvider dBScriptsProvider)
        {
            sqlServerConnection.ThrowIfNull(nameof(sqlServerConnection));

            _sqlServerConnection = sqlServerConnection;
            _dBScriptsProvider = dBScriptsProvider;

            _sqlServerConnection.Open();
        }


        public int GetNumOfOpenConnection(string dbName)
        {
            int outVal = 0;

            lock (_sqlServerConnection)
            {
                if (!_sqlServerConnection.IsDisposed)
                {
                    string sqlCommandStr = _dBScriptsProvider.GetNumOfOpenConnectionScript(dbName);

                    using (DataTable resultsTable = _sqlServerConnection.GetSelectCommand(sqlCommandStr, 10))
                    {
                        if (resultsTable.Rows.Count > 0)
                        {
                            DataRow statusRow = resultsTable.Rows[0];
                            outVal = Convert.ToInt32(statusRow["NumberOfConnections"], CultureInfo.InvariantCulture);
                        }
                    }


                }
            }

            return outVal;
        }




        public double GetBackupProcessStatus()
        {
            return GetQueryProcessStatus("BACKUP DATABASE");
        }

        public double GetRestoreProcessStatus()
        {
            return GetQueryProcessStatus("RESTORE DATABASE");
        }


        private double GetQueryProcessStatus(string queryName)
        {
            double outVal = 0;

            lock (_sqlServerConnection)
            {
                if (!_sqlServerConnection.IsDisposed)
                {
                    string sqlCommandStr = _dBScriptsProvider.GetQueryProcessStatusScript(queryName);

                    using (DataTable resultsTable = _sqlServerConnection.GetSelectCommand(sqlCommandStr, 10))
                    {
                        if (resultsTable.Rows.Count > 0)
                        {
                            DataRow statusRow = resultsTable.Rows[0];
                            outVal = Convert.ToDouble(statusRow["ProgressPrecentage"], CultureInfo.InvariantCulture);
                        }
                    }
                }
            }




            return outVal;
        }


        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SqlServerDBQueryStatus()
        {
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_sqlServerConnection)
                {
                    _sqlServerConnection.Close();

                    _sqlServerConnection.Dispose();

                }
            }
            // free native resources here if there are any
        }

        #endregion

    }
}
