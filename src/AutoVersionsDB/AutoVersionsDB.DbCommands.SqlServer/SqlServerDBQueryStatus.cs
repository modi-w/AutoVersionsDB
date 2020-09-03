using AutoVersionsDB.Common;
using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Data;
using System.Globalization;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBQueryStatus : IDBQueryStatus
    {
        private readonly SqlServerConnection _sqlServerConnection;



        public SqlServerDBQueryStatus(SqlServerConnection sqlServerConnection)
        {
            sqlServerConnection.ThrowIfNull(nameof(sqlServerConnection));

            _sqlServerConnection = sqlServerConnection;

            _sqlServerConnection.Open();
        }


        public int GetNumOfOpenConnection(string dbName)
        {
            int outVal = 0;

            lock (_sqlServerConnection)
            {
                if (!_sqlServerConnection.IsDisposed)
                {
                    string sqlCommandStr =
                            $@"SELECT 
                                DB_NAME(dbid) as DBName, 
                                COUNT(dbid) as NumberOfConnections,
                                loginame as LoginName
                            FROM
                                sys.sysprocesses
                            WHERE 
                                dbid > 0
                                AND  DB_NAME(dbid) = '{dbName}'
                            GROUP BY 
                                dbid, loginame";


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
                    string sqlCommandStr =
                        $@"SELECT session_id as SPID, command, a.text AS Query, start_time, percent_complete, dateadd(second,estimated_completion_time/1000, getdate()) as estimated_completion_time
                    FROM sys.dm_exec_requests r CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) a
                    WHERE r.command in ('{queryName}')";


                    using (DataTable resultsTable = _sqlServerConnection.GetSelectCommand(sqlCommandStr, 10))
                    {
                        if (resultsTable.Rows.Count > 0)
                        {
                            DataRow statusRow = resultsTable.Rows[0];
                            outVal = Convert.ToDouble(statusRow["percent_complete"], CultureInfo.InvariantCulture);
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
