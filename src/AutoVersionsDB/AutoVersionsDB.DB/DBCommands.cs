using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.DB
{
    public class DBCommands : IDisposable
    {
        private readonly IDBConnection _dbConnection;
        private readonly IDBScriptsProvider _dBScriptsProvider;



        public DBCommands(IDBConnection dbConnection,
                                    IDBScriptsProvider dBScriptsProvider)
        {
            dbConnection.ThrowIfNull(nameof(dbConnection));

            _dbConnection = dbConnection;
            _dBScriptsProvider = dBScriptsProvider;

            _dbConnection.Open();
        }




        public string DataBaseName
        {
            get
            {
                string dbName;

                dbName = _dbConnection.DataBaseName;

                return dbName;
            }
        }

        public DataSet GetScriptsExecutionHistoryTableStructureFromDB()
        {
            DataSet dsExecutionHistory = new DataSet();

            string sqlCmdStr = _dBScriptsProvider.GetEmptyTableScript(DBCommandsConsts.DbScriptsExecutionHistoryFullTableName);
            DataTable dbScriptsExecutionHistoryTable = _dbConnection.GetSelectCommand(sqlCmdStr);
            dbScriptsExecutionHistoryTable.TableName = DBCommandsConsts.DbScriptsExecutionHistoryFullTableName;
            dsExecutionHistory.Tables.Add(dbScriptsExecutionHistoryTable);


            sqlCmdStr = _dBScriptsProvider.GetEmptyTableScript(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName);
            DataTable dbScriptsExecutionHistoryFilesTable = _dbConnection.GetSelectCommand(sqlCmdStr);
            dbScriptsExecutionHistoryFilesTable.TableName = DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName;
            dsExecutionHistory.Tables.Add(dbScriptsExecutionHistoryFilesTable);


            return dsExecutionHistory;
        }

        public void UpdateScriptsExecutionHistoryTableToDB(DataTable dbScriptsExecutionHistoryTable)
        {
            _dbConnection.UpdateDataTableWithUpdateIdentityOnInsert(dbScriptsExecutionHistoryTable);
        }

        public void UpdateScriptsExecutionHistoryFilesTableToDB(DataTable dbScriptsExecutionHistoryFilesTable)
        {
            _dbConnection.UpdateDataTableWithUpdateIdentityOnInsert(dbScriptsExecutionHistoryFilesTable);
        }



        public DataTable GetExecutedFilesFromDBByFileTypeCode(string scriptFileType)
        {
            DataTable executedFilesFromDBTable;

            string sqlCmdStr = _dBScriptsProvider.GetExecutedFilesFromDBByFileTypeCodeScript(DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName, scriptFileType);
            executedFilesFromDBTable = _dbConnection.GetSelectCommand(sqlCmdStr);

            return executedFilesFromDBTable;
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
            _dbConnection.ExecSQLCommandStr(commandStr);
        }


        public bool CheckIfTableExist(string schemaName, string tableName)
        {
            schemaName.ThrowIfNull(nameof(schemaName));
            tableName.ThrowIfNull(nameof(tableName));

            bool outVal = false;

            string sqlCmdStr = _dBScriptsProvider.CheckIfTableExistScript(schemaName, tableName);

            using (DataTable resultsTable = _dbConnection.GetSelectCommand(sqlCmdStr, 10))
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

            string sqlCmdStr = _dBScriptsProvider.CheckIfStoredProcedureExistScript(schemaName, spName);

            using (DataTable resultsTable = _dbConnection.GetSelectCommand(sqlCmdStr))
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

            string sqlCmdStr = _dBScriptsProvider.GetAllTableDataScript(tableName);

            outDt = _dbConnection.GetSelectCommand(sqlCmdStr);

            outDt.TableName = tableName;

            return outDt;
        }


        public DataTable GetAllDBSchemaExceptDBVersionSchema()
        {
            string sqlCmdStr = _dBScriptsProvider.GetAllDBTablesExceptSchemaScript(DBCommandsConsts.DbSchemaName);

            DataTable dbSchemaExceptDBVersionTable = _dbConnection.GetSelectCommand(sqlCmdStr);
            return dbSchemaExceptDBVersionTable;
        }


        public void RecreateDBVersionsTables()
        {
            string sqlCmdStr = _dBScriptsProvider.RecreateDBVersionsTablesScript();

            ExecSQLCommandStr(sqlCmdStr);
        }



        public void DropAllDBObject()
        {
            string sqlCmdStr = _dBScriptsProvider.DropAllDBObjectsScript();

            ExecSQLCommandStr(sqlCmdStr);

        }







        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DBCommands()
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
