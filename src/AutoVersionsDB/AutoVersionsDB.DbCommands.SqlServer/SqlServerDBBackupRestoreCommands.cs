using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Data;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBBackupRestoreCommands : IDBBackupRestoreCommands
    {
        private SqlServerConnectionManager _sqlServerConnectionManager;



        public SqlServerDBBackupRestoreCommands(SqlServerConnectionManager sqlServerConnectionManager)
        {
            _sqlServerConnectionManager = sqlServerConnectionManager;

            _sqlServerConnectionManager.Open();
        }


        public void CreateDbBackup(string filename, string dbName)
        {
            string sqlCommandStr = $"BACKUP DATABASE [{dbName}] TO DISK='{filename}'";

            _sqlServerConnectionManager.ExecSQLCommandStr(sqlCommandStr);
        }


        public void RestoreDbFromBackup(string filename, string dbName)
        {
            //_sqlServerConnectionManager.Close();
            //_sqlServerConnectionManager.Open();

            resolveDBInSingleUserMode(dbName);

            string sqlCmdStr = $"ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
            _sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr);

            sqlCmdStr = $"RESTORE DATABASE [{dbName}] FROM DISK='{filename}' WITH REPLACE;";
            _sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr);

            //_sqlServerConnectionManager.Close();

            //_sqlServerConnectionManager.Open();

            sqlCmdStr = $"ALTER DATABASE [{dbName}] SET MULTI_USER";
            _sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr);

            //_sqlServerConnectionManager.Close();
        }

        private void resolveDBInSingleUserMode(string dbName)
        {
            bool isDBInSigleUserMode = false;

            string sqlCmdStr2 = $"SELECT user_access_desc FROM sys.databases WHERE name = '{dbName}'";
            DataTable dbStateTable = _sqlServerConnectionManager.GetSelectCommand(sqlCmdStr2);

            if (dbStateTable.Rows.Count > 0)
            {
                DataRow dbStateRow = dbStateTable.Rows[0];

                isDBInSigleUserMode = Convert.ToString(dbStateRow["user_access_desc"]) == "SINGLE_USER";
            }

            if (isDBInSigleUserMode)
            {
                sqlCmdStr2 = $"SELECT request_session_id FROM sys.dm_tran_locks WHERE resource_database_id = DB_ID('{dbName}')";
                DataTable sessionsTable = _sqlServerConnectionManager.GetSelectCommand(sqlCmdStr2);

                foreach (DataRow rowSession in sessionsTable.Rows)
                {
                    string seesionID = Convert.ToString(rowSession["request_session_id"]);
                    sqlCmdStr2 = $"KILL {seesionID}";
                    _sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr2);
                }

                //Comment: we prefer to drop db and create again becauase if the db stuck on restore state we couldnt change it to MULTI_USER
                //sqlCmdStr2 = $"ALTER DATABASE [{dbName}] SET MULTI_USER ";
                //_sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr2);

                sqlCmdStr2 = string.Format("DROP DATABASE [" + dbName + "]");
                _sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr2);

                sqlCmdStr2 = string.Format("CREATE DATABASE [" + dbName + "]");
                _sqlServerConnectionManager.ExecSQLCommandStr(sqlCmdStr2);
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
                _sqlServerConnectionManager.Close();

                _sqlServerConnectionManager.Dispose();
            }
            // free native resources here if there are any
        }

        #endregion

    }
}
