using AutoVersionsDB.Helpers;
using System;
using System.Data;
using System.Globalization;
using System.IO;

namespace AutoVersionsDB.DbCommands.Contract
{
    public class DBBackupRestoreCommands : IDisposable
    {
        private readonly IDBConnection _dbConnection;
        private readonly IDBScriptsProvider _dBScriptsProvider;



        public DBBackupRestoreCommands(IDBConnection dbConnection,
                                                IDBScriptsProvider dBScriptsProvider)
        {
            dbConnection.ThrowIfNull(nameof(dbConnection));

            _dbConnection = dbConnection;
            _dBScriptsProvider = dBScriptsProvider;

            _dbConnection.Open();
        }


        public void CreateDBBackup(string filename, string dbName)
        {
            string sqlCmdStr = _dBScriptsProvider.BackupDBScript(filename, dbName);

            _dbConnection.ExecSQLCommandStr(sqlCmdStr);
        }


        public void RestoreDBFromBackup(string filename, string dbName, string dbFilesBasePath = null)
        {
            ResolveDBInSingleUserMode(dbName, dbFilesBasePath);

            string sqlCmdStr = _dBScriptsProvider.RestorDBScript(filename, dbName, dbFilesBasePath);

            _dbConnection.ExecSQLCommandStr(sqlCmdStr);
        }

        private void ResolveDBInSingleUserMode(string dbName, string dbFilesBasePath)
        {
            bool isDBInSigleUserMode = false;

            string sqlCommandStr = _dBScriptsProvider.GetDBAccessStateScript(dbName);

            using (DataTable dbStateTable = _dbConnection.GetSelectCommand(sqlCommandStr))
            {
                isDBInSigleUserMode = dbStateTable.Rows.Count > 0;
            }

            if (isDBInSigleUserMode)
            {
                sqlCommandStr = _dBScriptsProvider.GetDBSessionsScript(dbName);

                using (DataTable sessionsTable = _dbConnection.GetSelectCommand(sqlCommandStr))
                {
                    foreach (DataRow rowSession in sessionsTable.Rows)
                    {
                        int seesionID = Convert.ToInt32(rowSession["SessionID"], CultureInfo.InvariantCulture);

                        sqlCommandStr = _dBScriptsProvider.KillSessionScript(seesionID.ToString());

                        _dbConnection.ExecSQLCommandStr(sqlCommandStr);
                    }
                }

                //Comment: we prefer to drop db and create again becauase if the db stuck on restore state we couldnt change it to MULTI_USER
                //sqlCmdStr2 = $"ALTER DATABASE [{dbName}] SET MULTI_USER ";
                //_sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr2);

                sqlCommandStr = _dBScriptsProvider.DropDBScript(dbName);
                _dbConnection.ExecSQLCommandStr(sqlCommandStr);

                CreateDB(dbName, dbFilesBasePath);
            }
            else
            {
                sqlCommandStr = _dBScriptsProvider.GetIsDBExsitScript(dbName);

                using (DataTable dtIsDBExsit = _dbConnection.GetSelectCommand(sqlCommandStr))
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
            string sqlCommandStr = _dBScriptsProvider.CreateDBScript(dbName, dbFilesBasePath);

            _dbConnection.ExecSQLCommandStr(sqlCommandStr);
        }



        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DBBackupRestoreCommands()
        {
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                _dbConnection.Close();

                _dbConnection.Dispose();
            }
            // free native resources here if there are any
        }

        #endregion

    }
}
