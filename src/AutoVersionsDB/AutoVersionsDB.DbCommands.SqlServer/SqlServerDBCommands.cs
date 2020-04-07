using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.SqlServer.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.DbCommands.SqlServer
{
    public class SqlServerDBCommands : IDBCommands
    {
        private SqlServerConnectionManager _sqlServerConnectionManager;
        private EmbeddedResourcesManager _embeddedResourcesManager;



        public SqlServerDBCommands(SqlServerConnectionManager sqlServerConnectionManager,
                                    EmbeddedResourcesManager embeddedResourcesManager)
        {
            _sqlServerConnectionManager = sqlServerConnectionManager;
            _embeddedResourcesManager = embeddedResourcesManager;

            _sqlServerConnectionManager.Open();
        }




        public string GetDataBaseName()
        {
            string dbName;

            dbName = _sqlServerConnectionManager.DataBaseName;

            return dbName;
        }



        public DataSet GetScriptsExecutionHistoryTableStructureFromDB()
        {
            DataSet dsExecutionHistory = new DataSet();

            DataTable dbScriptsExecutionHistoryTable = _sqlServerConnectionManager.GetSelectCommand(string.Format("select * from {0} where 1=2", DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName));
            dbScriptsExecutionHistoryTable.TableName = DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName;
            dsExecutionHistory.Tables.Add(dbScriptsExecutionHistoryTable);

            DataTable dbScriptsExecutionHistoryFilesTable = _sqlServerConnectionManager.GetSelectCommand(string.Format("select * from {0} where 1=2", DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName));
            dbScriptsExecutionHistoryFilesTable.TableName = DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName;
            dsExecutionHistory.Tables.Add(dbScriptsExecutionHistoryFilesTable);

            return dsExecutionHistory;
        }

        public void UpdateScriptsExecutionHistoryTableToDB(DataTable dbScriptsExecutionHistoryTable)
        {
            _sqlServerConnectionManager.UpdateDataTableWithUpdateIdentityOnInsert(dbScriptsExecutionHistoryTable);
        }

        public void UpdateScriptsExecutionHistoryFilesTableToDB(DataTable dbScriptsExecutionHistoryFilesTable)
        {
            _sqlServerConnectionManager.UpdateDataTableWithUpdateIdentityOnInsert(dbScriptsExecutionHistoryFilesTable);
        }



        public DataTable GetExecutedFilesFromDBByFileTypeCode(string scriptFileType)
        {
            DataTable aExecutedFilesFromDBTable;

            string sqlCommand = $" SELECT * " +
               $"               FROM {DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName} " +
               $"               WHERE ScriptFileType='{scriptFileType}'"  +
               $"               ORDER BY [ID]";

            aExecutedFilesFromDBTable = _sqlServerConnectionManager.GetSelectCommand(sqlCommand);

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
            _sqlServerConnectionManager.ExecSQLCommandStr(commandStr);
        }


        public bool CheckIfTableExist(string schemaName, string tableName)
        {
            bool outVal = false;

            string sqlCommandStr = string.Format(
                @"SELECT * 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_SCHEMA = '{0}' 
                    AND  TABLE_NAME = '{1}'", schemaName.Trim('[').Trim(']'), tableName.Trim('[').Trim(']'));

            DataTable resultsTable = _sqlServerConnectionManager.GetSelectCommand(sqlCommandStr, 10);

            if (resultsTable.Rows.Count > 0)
            {
                outVal = true;
            }

            return outVal;
        }

        public bool CheckIfStoredProcedureExist(string schemaName, string spName)
        {
            bool outVal = false;

            string sqlCommandStr = $"   SELECT QUOTENAME(tbSchemas.name) AS SchemaName, QUOTENAME(tbObjects.name) AS ObjectName " +
                   $"   FROM sys.objects AS tbObjects " +
                   $"       JOIN sys.schemas tbSchemas ON tbSchemas.schema_id = tbObjects.schema_id " +
                   $"   WHERE tbSchemas.name = '{schemaName}'" +
                   $"       AND tbObjects.name='{spName}'" +
                   $"       AND TYPE='P'";

            DataTable resultsTable = _sqlServerConnectionManager.GetSelectCommand(sqlCommandStr);

            if (resultsTable.Rows.Count > 0)
            {
                outVal = true;
            }

            return outVal;
        }

        public DataTable GetTable(string tableName)
        {
            DataTable outDt;

            string selectCommand = string.Format("SELECT * FROM {0}", tableName);
            outDt = _sqlServerConnectionManager.GetSelectCommand(selectCommand);

            outDt.TableName = tableName;

            return outDt;
        }


        public DataTable GetAllDBSchemaExceptDBVersionSchema()
        {
            DataTable dbSchemaExceptDBVersionTable = null;

            string sqlCmdStr = $"   SELECT QUOTENAME(tbSchemas.name) AS SchemaName, QUOTENAME(tbObjects.name) AS ObjectName " +
                                   $"   FROM sys.objects AS tbObjects " +
                                   $"       JOIN sys.schemas tbSchemas ON tbSchemas.schema_id = tbObjects.schema_id " +
                                   $"   WHERE tbSchemas.name <> '{DBCommandsConsts.C_DB_SchemaName}'" +
                                   $"       AND (TYPE='U' OR TYPE='TF' OR TYPE='SF' OR TYPE='AF' OR TYPE='P')";

            dbSchemaExceptDBVersionTable = _sqlServerConnectionManager.GetSelectCommand(sqlCmdStr);

            return dbSchemaExceptDBVersionTable;
        }


        public void RecreateDBVersionsTables()
        {
            string recreateDBVersionsSchema =
                _embeddedResourcesManager.GetEmbeddedResourceFile("AutoVersionsDB.DbCommands.SqlServer.SystemScripts.RecreateDBVersionsSchema_SqlServer.sql");

            ExecSQLCommandStr(recreateDBVersionsSchema);
        }



        public void DropAllDB()
        {
            string recreateDBVersionsSchema =
                _embeddedResourcesManager.GetEmbeddedResourceFile("AutoVersionsDB.DbCommands.SqlServer.SystemScripts.DropAllDbObjects_SqlServer.sql");

            ExecSQLCommandStr(recreateDBVersionsSchema);

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
                _sqlServerConnectionManager.Close();

                _sqlServerConnectionManager.Dispose();
            }
            // free native resources here if there are any
        }

        #endregion

    }
}
