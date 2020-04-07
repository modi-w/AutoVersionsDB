using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Data;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBQueryStatus : IDBQueryStatus
    {
        private SqlServerConnectionManager _sqlServerConnectionManager;



        public SqlServerDBQueryStatus(SqlServerConnectionManager sqlServerConnectionManager)
        {
            _sqlServerConnectionManager = sqlServerConnectionManager;

            _sqlServerConnectionManager.Open();
        }


        public int GetNumOfOpenConnection(string dbName)
        {
            int outVal = 0;

            lock (_sqlServerConnectionManager)
            {
                if (!_sqlServerConnectionManager.IsDisposed)
                {
                    string sqlCommandStr = string.Format(
                            @"SELECT 
                                DB_NAME(dbid) as DBName, 
                                COUNT(dbid) as NumberOfConnections,
                                loginame as LoginName
                            FROM
                                sys.sysprocesses
                            WHERE 
                                dbid > 0
                                AND  DB_NAME(dbid) = '{0}'
                            GROUP BY 
                                dbid, loginame", dbName);


                    DataTable resultsTable = _sqlServerConnectionManager.GetSelectCommand(sqlCommandStr, 10);

                    if (resultsTable.Rows.Count > 0)
                    {
                        DataRow statusRow = resultsTable.Rows[0];
                        outVal = Convert.ToInt32(statusRow["NumberOfConnections"]);
                    }
                }
            }

            return outVal;
        }




        public double GetBackupProcessStatus()
        {
            return getQueryProcessStatus("BACKUP DATABASE");
        }

        public double GetRestoreProcessStatus()
        {
            return getQueryProcessStatus("RESTORE DATABASE");
        }


        private double getQueryProcessStatus(string queryName)
        {
            double outVal = 0;

            lock (_sqlServerConnectionManager)
            {
                if (!_sqlServerConnectionManager.IsDisposed)
                {
                    string sqlCommandStr = string.Format(
                        @"SELECT session_id as SPID, command, a.text AS Query, start_time, percent_complete, dateadd(second,estimated_completion_time/1000, getdate()) as estimated_completion_time
                    FROM sys.dm_exec_requests r CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) a
                    WHERE r.command in ('{0}')", queryName);


                    DataTable resultsTable = _sqlServerConnectionManager.GetSelectCommand(sqlCommandStr, 10);

                    if (resultsTable.Rows.Count > 0)
                    {
                        DataRow statusRow = resultsTable.Rows[0];
                        outVal = Convert.ToDouble(statusRow["percent_complete"]);
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
                lock (_sqlServerConnectionManager)
                {
                    _sqlServerConnectionManager.Close();

                    _sqlServerConnectionManager.Dispose();

                }
            }
            // free native resources here if there are any
        }

        #endregion

    }
}
