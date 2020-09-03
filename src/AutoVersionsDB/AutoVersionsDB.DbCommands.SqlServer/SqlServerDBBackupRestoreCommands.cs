using AutoVersionsDB.Common;
using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Data;
using System.Globalization;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBBackupRestoreCommands : IDBBackupRestoreCommands, IDisposable
    {
        private readonly SqlServerConnection _sqlServerConnection;



        public SqlServerDBBackupRestoreCommands(SqlServerConnection sqlServerConnection)
        {
            sqlServerConnection.ThrowIfNull(nameof(sqlServerConnection));

            _sqlServerConnection = sqlServerConnection;

            _sqlServerConnection.Open();
        }


        public void CreateDbBackup(string filename, string dbName)
        {
            string sqlCommandStr = $"BACKUP DATABASE [{dbName}] TO DISK='{filename}'";

            _sqlServerConnection.ExecSQLCommandStr(sqlCommandStr);
        }


        public void RestoreDbFromBackup(string filename, string dbName, string dbFilesBasePath = null)
        {
            //_sqlServerConnectionManager.Close();
            //_sqlServerConnectionManager.Open();

            ResolveDBInSingleUserMode(dbName, dbFilesBasePath);

            string sqlCmdStr = $"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            _sqlServerConnection.ExecSQLCommandStr(sqlCmdStr);

            sqlCmdStr = $"RESTORE DATABASE [{dbName}] FROM DISK='{filename}' WITH REPLACE";// ;";
            if (!string.IsNullOrWhiteSpace(dbFilesBasePath))
            {
                sqlCmdStr += $@", MOVE '{dbName}' TO '{dbFilesBasePath}\{dbName}.mdf', ";
                sqlCmdStr += $@" MOVE '{dbName}_log' TO '{dbFilesBasePath}\{dbName}.ldf';";
            }

            _sqlServerConnection.ExecSQLCommandStr(sqlCmdStr);

            //_sqlServerConnectionManager.Close();

            //_sqlServerConnectionManager.Open();

            sqlCmdStr = $"ALTER DATABASE [{dbName}] SET MULTI_USER";
            _sqlServerConnection.ExecSQLCommandStr(sqlCmdStr);

            //_sqlServerConnectionManager.Close();
        }

        private void ResolveDBInSingleUserMode(string dbName, string dbFilesBasePath)
        {
            bool isDBInSigleUserMode = false;

            string sqlCmdStr2 = $"SELECT user_access_desc, state_desc FROM sys.databases WHERE name = '{dbName}'";
            using (DataTable dbStateTable = _sqlServerConnection.GetSelectCommand(sqlCmdStr2))
            {
                if (dbStateTable.Rows.Count > 0)
                {
                    DataRow dbStateRow = dbStateTable.Rows[0];

                    isDBInSigleUserMode = Convert.ToString(dbStateRow["user_access_desc"], CultureInfo.InvariantCulture) == "SINGLE_USER"
                                    || Convert.ToString(dbStateRow["state_desc"], CultureInfo.InvariantCulture) == "RESTORING";


                }
            }

            if (isDBInSigleUserMode)
            {
                sqlCmdStr2 = $"SELECT request_session_id FROM sys.dm_tran_locks WHERE resource_database_id = DB_ID('{dbName}')";

                using (DataTable sessionsTable = _sqlServerConnection.GetSelectCommand(sqlCmdStr2))
                {
                    foreach (DataRow rowSession in sessionsTable.Rows)
                    {
                        int seesionID = Convert.ToInt32(rowSession["request_session_id"], CultureInfo.InvariantCulture);
                        if (seesionID > 50)
                        {
                            sqlCmdStr2 = $"KILL {seesionID}";
                            _sqlServerConnection.ExecSQLCommandStr(sqlCmdStr2);
                        }
                    }
                }

                //Comment: we prefer to drop db and create again becauase if the db stuck on restore state we couldnt change it to MULTI_USER
                //sqlCmdStr2 = $"ALTER DATABASE [{dbName}] SET MULTI_USER ";
                //_sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr2);

                sqlCmdStr2 = $"DROP DATABASE [{dbName}]";
                _sqlServerConnection.ExecSQLCommandStr(sqlCmdStr2);

                sqlCmdStr2 = $"CREATE DATABASE [{dbName}]";
                if (!string.IsNullOrWhiteSpace(dbFilesBasePath))
                {
                    sqlCmdStr2 += " ON ";
                    sqlCmdStr2 += $@" ( NAME = '{dbName}_dat', FILENAME = '{dbFilesBasePath}\{dbName}.mdf') ";
                    sqlCmdStr2 += " LOG ON ";
                    sqlCmdStr2 += $@" (NAME = '{dbName}_log', FILENAME = '{dbFilesBasePath}\{dbName}.ldf') ";
                }
                _sqlServerConnection.ExecSQLCommandStr(sqlCmdStr2);
            }
            else
            {
                sqlCmdStr2 = $"SELECT name FROM master.dbo.sysdatabases WHERE ('[' + name + ']' = '{dbName}' OR name = '{dbName}' )";

                using (DataTable dtIsDBExsit = _sqlServerConnection.GetSelectCommand(sqlCmdStr2))
                {
                    if (dtIsDBExsit.Rows.Count == 0)
                    {
                        //sqlCmdStr2 = @"DECLARE @dataFilePath NVARCHAR(MAX) = CAST(SERVERPROPERTY('InstanceDefaultDataPath') AS NVARCHAR) + FORMATMESSAGE('\%s.mdf', 'MASTER'); SELECT @dataFilePath ;";
                        //DataTable DT = _sqlServerConnectionManager.GetSelectCommand(sqlCmdStr2);


                        sqlCmdStr2 = $"CREATE DATABASE [{dbName}]";

                        if (!string.IsNullOrWhiteSpace(dbFilesBasePath))
                        {
                            sqlCmdStr2 += " ON ";
                            sqlCmdStr2 += $@" ( NAME = '{dbName}_dat', FILENAME = '{dbFilesBasePath}\{dbName}.mdf') ";
                            sqlCmdStr2 += " LOG ON ";
                            sqlCmdStr2 += $@" (NAME = '{dbName}_log', FILENAME = '{dbFilesBasePath}\{dbName}.ldf') ";
                        }

                        _sqlServerConnection.ExecSQLCommandStr(sqlCmdStr2);
                    }
                }
            }
        }





        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SqlServerDBBackupRestoreCommands()
        {
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                _sqlServerConnection.Close();

                _sqlServerConnection.Dispose();
            }
            // free native resources here if there are any
        }

        #endregion

    }
}
