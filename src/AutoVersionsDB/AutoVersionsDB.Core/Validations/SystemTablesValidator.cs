using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.DbCommands.Contract;
using System;
using System.Data;

namespace AutoVersionsDB.Core.Validations
{
    public class SystemTablesValidator : ValidatorBase
    {
        public override string ValidatorName => "SystemTables";

        public override string ErrorInstructionsMessage
        {
            get
            {
                if (_isDevEnvironment)
                {
                    return "The system tables has invalid structure. Please try to 'Recreate DB From Scratch' or 'Set DB State by Virtual Execution'.";
                }
                else
                {
                    return "The system tables has invalid structure. Please try to 'Set DB State by Virtual Execution'.";
                }
            }
        }

        private IDBCommands _dbCommands;
        private bool _isDevEnvironment;

        public SystemTablesValidator(IDBCommands dbCommands, bool isDevEnvironment)
        {
            _dbCommands = dbCommands;
            _isDevEnvironment = isDevEnvironment;
        }

        public override string Validate(AutoVersionsDBExecutionParams executionParam)
        {
            if (!_dbCommands.CheckIfTableExist(DBCommandsConsts.C_DB_SchemaName, DBCommandsConsts.C_DBScriptsExecutionHistory_TableName))
            {
                string errorMsg = $"The table '{DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName}' is not exist in the db";
                return errorMsg;
            }

            if (!_dbCommands.CheckIfTableExist(DBCommandsConsts.C_DB_SchemaName, DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_TableName))
            {
                string errorMsg = $"The table '{DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_TableName}' is not exist in the db";
                return errorMsg;
            }


            DataSet systemTablesSetFromDB = _dbCommands.GetScriptsExecutionHistoryTableStructureFromDB();

            DataTable scriptsExecutionHistoryTableFromDB = systemTablesSetFromDB.Tables[DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName];
            DataTable scriptsExecutionHistoryTableFromStructure = CreateScriptsExecutionHistoryTableStructure();

            foreach (DataColumn colFromStruct in scriptsExecutionHistoryTableFromStructure.Columns)
            {
                if (!scriptsExecutionHistoryTableFromDB.Columns.Contains(colFromStruct.ColumnName))
                {
                    string errorMsg = $"The table '{DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName}' is missing the column '{colFromStruct}'";
                    return errorMsg;
                }
                else
                {
                    DataColumn colFromDB = scriptsExecutionHistoryTableFromDB.Columns[colFromStruct.ColumnName];
                    if (colFromDB.DataType != colFromStruct.DataType)
                    {
                        string errorMsg = $"The column '{colFromStruct.ColumnName}' has the type '{colFromDB.DataType}' instead of '{colFromStruct.DataType}', in the table {DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName}";
                        return errorMsg;
                    }
                }
            }


            DataTable scriptsExecutionHistoryFilesTableFromDB = systemTablesSetFromDB.Tables[DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName];
            DataTable scriptsExecutionHistoryFilesTableFromStructure = CreateScriptsExecutionHistoryFilesTableStructure();

            foreach (DataColumn colFromStruct in scriptsExecutionHistoryFilesTableFromStructure.Columns)
            {
                if (!scriptsExecutionHistoryFilesTableFromDB.Columns.Contains(colFromStruct.ColumnName))
                {
                    string errorMsg = $"The table '{DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName}' is missing the column '{colFromStruct}'";
                    return errorMsg;
                }
                else
                {
                    DataColumn colFromDB = scriptsExecutionHistoryFilesTableFromStructure.Columns[colFromStruct.ColumnName];
                    if (colFromDB.DataType != colFromStruct.DataType)
                    {
                        string errorMsg = $"The column '{colFromStruct.ColumnName}' has the type '{colFromDB.DataType}' instead of '{colFromStruct.DataType}', in the table {DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName}";
                        return errorMsg;
                    }
                }
            }


            return "";
        }



        private DataTable CreateScriptsExecutionHistoryTableStructure()
        {
            DataTable tableResults = new DataTable();

            tableResults.Columns.Add("DBScriptsExecutionHistoryID", typeof(int));
            tableResults.Columns.Add("ExecutionTypeName", typeof(string));
            tableResults.Columns.Add("StartProcessDateTime", typeof(DateTime));
            tableResults.Columns.Add("EndProcessDateTime", typeof(DateTime));
            tableResults.Columns.Add("ProcessDurationInMs", typeof(double));
            tableResults.Columns.Add("IsVirtualExecution", typeof(bool));
            tableResults.Columns.Add("NumOfScriptFiles", typeof(int));
            tableResults.Columns.Add("DBBackupFileFullPath", typeof(string));

            return tableResults;
        }

        private DataTable CreateScriptsExecutionHistoryFilesTableStructure()
        {
            DataTable tableResults = new DataTable();

            tableResults.Columns.Add("ID", typeof(int));
            tableResults.Columns.Add("DBScriptsExecutionHistoryID", typeof(int));
            tableResults.Columns.Add("ExecutedDateTime", typeof(DateTime));
            tableResults.Columns.Add("IsVirtualExecution", typeof(bool));
            tableResults.Columns.Add("Filename", typeof(string));
            tableResults.Columns.Add("FileFullPath", typeof(string));
            tableResults.Columns.Add("ScriptFileType", typeof(string));
            tableResults.Columns.Add("ComputedFileHash", typeof(string));
            tableResults.Columns.Add("ComputedFileHashDateTime", typeof(DateTime));

            return tableResults;
        }

    }
}
