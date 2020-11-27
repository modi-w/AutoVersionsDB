using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBCommands : IDBCommands, IDisposable
    {
        private readonly SqlServerConnection _sqlServerConnection;



        public SqlServerDBCommands(SqlServerConnection sqlServerConnection)
        {
            sqlServerConnection.ThrowIfNull(nameof(sqlServerConnection));

            _sqlServerConnection = sqlServerConnection;

            _sqlServerConnection.Open();
        }




        public string GetDataBaseName()
        {
            string dbName;

            dbName = _sqlServerConnection.DataBaseName;

            return dbName;
        }


        public DataTable GetEmptyTable(string tableName)
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetEmptyTable_SqlServer.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{tableName}", tableName);

            DataTable dataTable = _sqlServerConnection.GetSelectCommand(sqlCmdStr);
            dataTable.TableName = tableName;

            return dataTable;
        }


        public void UpdateScriptsExecutionToDB(ScriptsExecution scriptsExecution)
        {
            DataTable dbScriptsExecutionHistoryTable=  GetEmptyTable(DBCommandsConsts.DbScriptsExecutionHistoryFullTableName);

            DataRow drScriptsExecutionHistory = dbScriptsExecutionHistoryTable.NewRow();
            ReflectionUtils.TryCopyPropertiesFromObjectToDataRow(scriptsExecution, drScriptsExecutionHistory);
            dbScriptsExecutionHistoryTable.Rows.Add(drScriptsExecutionHistory);

            _sqlServerConnection.UpdateDataTableWithUpdateIdentityOnInsert(dbScriptsExecutionHistoryTable);

            int dbScriptsExecutionHistoryID = Convert.ToInt32(drScriptsExecutionHistory["DBScriptsExecutionHistoryID"], CultureInfo.InvariantCulture);


            DataTable dbScriptsExecutionHistoryFilesTable = GetEmptyTable(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);

            int currentID = -1;
            foreach (var scriptsExecutionFile in scriptsExecution.ScriptsExecutionFiles)
            {
                scriptsExecutionFile.ID = currentID--;
                scriptsExecutionFile.DBScriptsExecutionHistoryID = dbScriptsExecutionHistoryID;

                DataRow drScriptsExecutionHistoryFile = dbScriptsExecutionHistoryFilesTable.NewRow();
                ReflectionUtils.TryCopyPropertiesFromObjectToDataRow(scriptsExecutionFile, drScriptsExecutionHistoryFile);
                dbScriptsExecutionHistoryFilesTable.Rows.Add(drScriptsExecutionHistoryFile);
            }

            _sqlServerConnection.UpdateDataTableWithUpdateIdentityOnInsert(dbScriptsExecutionHistoryFilesTable);
        }

      

        public void UpdateScriptsExecutionHistoryFilesTableToDB(DataTable dbScriptsExecutionHistoryFilesTable)
        {
            _sqlServerConnection.UpdateDataTableWithUpdateIdentityOnInsert(dbScriptsExecutionHistoryFilesTable);
        }



        public DataTable GetExecutedFilesFromDBByFileTypeCode(string scriptFileType)
        {
            DataTable aExecutedFilesFromDBTable;

            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetExecutedFilesFromDBByFileTypeCode_SqlServer.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{executedFilesTableName}", DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName)
                .Replace("{scriptFileType}", scriptFileType);
            
            aExecutedFilesFromDBTable = _sqlServerConnection.GetSelectCommand(sqlCmdStr);

            return aExecutedFilesFromDBTable;
        }


        public IEnumerable<string> SplitSqlStatementsToExecutionBlocks(string sqlUnifyScript)
        {
            // Split by "GO" statements
            var statements = Regex.Split(
                    sqlUnifyScript,
                    @"^[\t\r\n]*GO[\t\r\n]*\d*[\t\r\n]*(?:--.*)?$",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            return statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'));
        }


        public void ExecSQLCommandStr(string commandStr)
        {
            _sqlServerConnection.ExecSQLCommandStr(commandStr);
        }


        public bool CheckIfTableExist(string schemaName, string tableName)
        {
            schemaName.ThrowIfNull(nameof(schemaName));
            tableName.ThrowIfNull(nameof(tableName));

            bool outVal = false;

            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("CheckIfTableExist_SqlServer.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{schemaName}", schemaName.Trim('[').Trim(']'))
                .Replace("{tableName}", tableName.Trim('[').Trim(']'));
            

            using (DataTable resultsTable = _sqlServerConnection.GetSelectCommand(sqlCmdStr, 10))
            {
                if (resultsTable.Rows.Count > 0)
                {
                    outVal = true;
                }
            }


            return outVal;
        }

        public bool CheckIfStoredProcedureExist(string schemaName, string spName)
        {
            bool outVal = false;
            
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("CheckIfStoredProcedureExist_SqlServer.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{schemaName}", schemaName)
                .Replace("{spName}", spName);

            using (DataTable resultsTable = _sqlServerConnection.GetSelectCommand(sqlCmdStr))
            {
                if (resultsTable.Rows.Count > 0)
                {
                    outVal = true;
                }
            }



            return outVal;
        }

        public DataTable GetTable(string tableName)
        {
            DataTable outDt;

            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetAllTableData_SqlServer.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{tableName}", tableName);

            outDt = _sqlServerConnection.GetSelectCommand(sqlCmdStr);

            outDt.TableName = tableName;

            return outDt;
        }


        public DataTable GetAllDBSchemaExceptDBVersionSchema()
        {
            string sqlCmdStr = GetEmbeddedResourceFileSqlServerScript("GetAllDBTablesExceptSchema_SqlServer.sql");
            sqlCmdStr =
                sqlCmdStr
                .Replace("{dbSchemaName}", DBCommandsConsts.DbSchemaName);

            DataTable dbSchemaExceptDBVersionTable = _sqlServerConnection.GetSelectCommand(sqlCmdStr);
            return dbSchemaExceptDBVersionTable;
        }


        public void RecreateDBVersionsTables()
        {
            string recreateDBVersionsSchema =
                GetEmbeddedResourceFileSqlServerScript("RecreateDBVersionsSchema_SqlServer.sql");

            ExecSQLCommandStr(recreateDBVersionsSchema);
        }



        public void DropAllDBObjects()
        {
            string recreateDBVersionsSchema =
                GetEmbeddedResourceFileSqlServerScript("DropAllDbObjects_SqlServer.sql");

            ExecSQLCommandStr(recreateDBVersionsSchema);

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

        ~SqlServerDBCommands()
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
