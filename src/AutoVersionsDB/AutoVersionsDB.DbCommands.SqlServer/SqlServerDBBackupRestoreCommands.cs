using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Data;
using System.Globalization;
using System.IO;

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
            string sqlCommandStr = GetEmbeddedResourceFileSqlServerScript("BackupDB_SqlServer.sql");
            sqlCommandStr =
                sqlCommandStr
                .Replace("{dbName}", dbName)
                .Replace("{filename}", filename);


            _sqlServerConnection.ExecSQLCommandStr(sqlCommandStr);
        }


        public void RestoreDbFromBackup(string filename, string dbName, string dbFilesBasePath = null)
        {
            ResolveDBInSingleUserMode(dbName, dbFilesBasePath);


            string sqlCommandStr = GetEmbeddedResourceFileSqlServerScript("RestorDB_SqlServer.sql");
            sqlCommandStr =
                sqlCommandStr
                .Replace("{dbName}", dbName)
                .Replace("{filename}", filename);


            string moveDBFilesScriptStr = "";
            if (!string.IsNullOrWhiteSpace(dbFilesBasePath))
            {
                moveDBFilesScriptStr += $@", MOVE '{dbName}' TO '{dbFilesBasePath}\{dbName}.mdf', ";
                moveDBFilesScriptStr += $@" MOVE '{dbName}_log' TO '{dbFilesBasePath}\{dbName}.ldf';";
            }

            sqlCommandStr =
                sqlCommandStr
                .Replace("{moveDBFilesScript}", moveDBFilesScriptStr);

            _sqlServerConnection.ExecSQLCommandStr(sqlCommandStr);
        }

        private void ResolveDBInSingleUserMode(string dbName, string dbFilesBasePath)
        {
            bool isDBInSigleUserMode = false;

            string sqlCommandStr = GetEmbeddedResourceFileSqlServerScript("GetDBAccessState_SqlServer.sql");
            sqlCommandStr =
                sqlCommandStr
                .Replace("{dbName}", dbName);

            using (DataTable dbStateTable = _sqlServerConnection.GetSelectCommand(sqlCommandStr))
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
                sqlCommandStr = GetEmbeddedResourceFileSqlServerScript("GetDBSessions_SqlServer.sql");
                sqlCommandStr =
                    sqlCommandStr
                    .Replace("{dbName}", dbName);

                using (DataTable sessionsTable = _sqlServerConnection.GetSelectCommand(sqlCommandStr))
                {
                    foreach (DataRow rowSession in sessionsTable.Rows)
                    {
                        int seesionID = Convert.ToInt32(rowSession["request_session_id"], CultureInfo.InvariantCulture);
                        if (seesionID > 50)
                        {
                            sqlCommandStr = GetEmbeddedResourceFileSqlServerScript("KillSession_SqlServer.sql");
                            sqlCommandStr =
                                sqlCommandStr
                                .Replace("{seesionID}", seesionID.ToString());

                            _sqlServerConnection.ExecSQLCommandStr(sqlCommandStr);
                        }
                    }
                }

                //Comment: we prefer to drop db and create again becauase if the db stuck on restore state we couldnt change it to MULTI_USER
                //sqlCmdStr2 = $"ALTER DATABASE [{dbName}] SET MULTI_USER ";
                //_sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr2);

                sqlCommandStr = GetEmbeddedResourceFileSqlServerScript("DropDB_SqlServer.sql");
                sqlCommandStr =
                    sqlCommandStr
                    .Replace("{dbName}", dbName);
                _sqlServerConnection.ExecSQLCommandStr(sqlCommandStr);

                CreateDB(dbName, dbFilesBasePath);
            }
            else
            {
                sqlCommandStr = GetEmbeddedResourceFileSqlServerScript("GetIsDBExsit_SqlServer.sql");
                sqlCommandStr =
                    sqlCommandStr
                    .Replace("{dbName}", dbName);

                using (DataTable dtIsDBExsit = _sqlServerConnection.GetSelectCommand(sqlCommandStr))
                {
                    if (dtIsDBExsit.Rows.Count == 0)
                    {
                        //sqlCmdStr2 = @"DECLARE @dataFilePath NVARCHAR(MAX) = CAST(SERVERPROPERTY('InstanceDefaultDataPath') AS NVARCHAR) + FORMATMESSAGE('\%s.mdf', 'MASTER'); SELECT @dataFilePath ;";
                        //DataTable DT = _sqlServerConnectionManager.GetSelectCommand(sqlCmdStr2);

                        if (!string.IsNullOrWhiteSpace(dbFilesBasePath))
                        {
                            string mdfFilePath = Path.Combine(dbFilesBasePath, $"{dbName}.mdf");
                            if (File.Exists(mdfFilePath))
                            {
                                File.Delete(mdfFilePath);
                            }

                            string ldfFilePath = Path.Combine(dbFilesBasePath, $"{dbName}.ldf");
                            if (File.Exists(ldfFilePath))
                            {
                                File.Delete(ldfFilePath);
                            }
                        }

                        CreateDB(dbName, dbFilesBasePath);
                    }
                }
            }
        }

        private void CreateDB(string dbName, string dbFilesBasePath)
        {
            string sqlCommandStr = GetEmbeddedResourceFileSqlServerScript("CreateDB_SqlServer.sql");
            sqlCommandStr =
                sqlCommandStr
                .Replace("{dbName}", dbName);

            string logFilesScriptStr = getLogFilesScript(dbName, dbFilesBasePath);
            sqlCommandStr =
                sqlCommandStr
                .Replace("{logFilesScript}", logFilesScriptStr);

            _sqlServerConnection.ExecSQLCommandStr(sqlCommandStr);
        }

        private static string getLogFilesScript(string dbName, string dbFilesBasePath)
        {
            string logFilesScriptStr = "";
            if (!string.IsNullOrWhiteSpace(dbFilesBasePath))
            {
                logFilesScriptStr += " ON ";
                logFilesScriptStr += $@" ( NAME = '{dbName}_dat', FILENAME = '{dbFilesBasePath}\{dbName}.mdf') ";
                logFilesScriptStr += " LOG ON ";
                logFilesScriptStr += $@" (NAME = '{dbName}_log', FILENAME = '{dbFilesBasePath}\{dbName}.ldf') ";
            }

            return logFilesScriptStr;
        }

        private string GetEmbeddedResourceFileSqlServerScript(string filename)
        {
            string sqlCommandStr =
                EmbeddedResources
                .GetEmbeddedResourceFile($"AutoVersionsDB.DbCommands.SqlServer.SystemScripts.{filename}");

            return sqlCommandStr;
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
