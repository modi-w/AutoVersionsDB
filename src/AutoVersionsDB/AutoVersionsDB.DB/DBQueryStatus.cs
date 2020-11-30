using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Data;
using System.Globalization;

namespace AutoVersionsDB.DB
{
    public class DBQueryStatus : IDisposable
    {
        private readonly IDBConnection _dbConnection;
        private readonly IDBScriptsProvider _dBScriptsProvider;



        public DBQueryStatus(IDBConnection dbConnection,
                                        IDBScriptsProvider dBScriptsProvider)
        {
            dbConnection.ThrowIfNull(nameof(dbConnection));

            _dbConnection = dbConnection;
            _dBScriptsProvider = dBScriptsProvider;

            _dbConnection.Open();
        }


        public int GetNumOfOpenConnection(string dbName)
        {
            int outVal = 0;

            lock (_dbConnection)
            {
                if (!_dbConnection.IsDisposed)
                {
                    string sqlCommandStr = _dBScriptsProvider.GetNumOfOpenConnectionScript(dbName);

                    using (DataTable resultsTable = _dbConnection.GetSelectCommand(sqlCommandStr, 10))
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

            lock (_dbConnection)
            {
                if (!_dbConnection.IsDisposed)
                {
                    string sqlCommandStr = _dBScriptsProvider.GetQueryProcessStatusScript(queryName);

                    using (DataTable resultsTable = _dbConnection.GetSelectCommand(sqlCommandStr, 10))
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

        ~DBQueryStatus()
        {
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_dbConnection)
                {
                    _dbConnection.Close();

                    _dbConnection.Dispose();

                }
            }
            // free native resources here if there are any
        }

        #endregion

    }
}
