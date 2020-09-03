using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
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

        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private readonly string _connStr;
        private readonly string _dbTypeCode;
        private readonly bool _isDevEnvironment;

        public SystemTablesValidator(DBCommandsFactoryProvider dbCommandsFactoryProvider,   
                                    string connStr,
                                    string dbTypeCode,
                                    bool isDevEnvironment)
        {
            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
            _connStr = connStr;
            _dbTypeCode = dbTypeCode;
            _isDevEnvironment = isDevEnvironment;
        }

        public override string Validate(AutoVersionsDbProcessParams executionParam)
        {
            using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(_dbTypeCode, _connStr, 0))
            {

                if (!dbCommands.CheckIfTableExist(DBCommandsConsts.DbSchemaName, DBCommandsConsts.DbScriptsExecutionHistoryTableName))
                {
                    string errorMsg = $"The table '{DBCommandsConsts.DbScriptsExecutionHistoryFullTableName}' is not exist in the db";
                    return errorMsg;
                }

                if (!dbCommands.CheckIfTableExist(DBCommandsConsts.DbSchemaName, DBCommandsConsts.DbScriptsExecutionHistoryFilesTableName))
                {
                    string errorMsg = $"The table '{DBCommandsConsts.DbScriptsExecutionHistoryFilesTableName}' is not exist in the db";
                    return errorMsg;
                }


                DataSet systemTablesSetFromDB = dbCommands.GetScriptsExecutionHistoryTableStructureFromDB();

                DataTable scriptsExecutionHistoryTableFromDB = systemTablesSetFromDB.Tables[DBCommandsConsts.DbScriptsExecutionHistoryFullTableName];

                using (DataTable scriptsExecutionHistoryTableFromStructure = CreateScriptsExecutionHistoryTableStructure())
                {
                    foreach (DataColumn colFromStruct in scriptsExecutionHistoryTableFromStructure.Columns)
                    {
                        if (!scriptsExecutionHistoryTableFromDB.Columns.Contains(colFromStruct.ColumnName))
                        {
                            string errorMsg = $"The table '{DBCommandsConsts.DbScriptsExecutionHistoryFullTableName}' is missing the column '{colFromStruct}'";
                            return errorMsg;
                        }
                        else
                        {
                            DataColumn colFromDB = scriptsExecutionHistoryTableFromDB.Columns[colFromStruct.ColumnName];
                            if (colFromDB.DataType != colFromStruct.DataType)
                            {
                                string errorMsg = $"The column '{colFromStruct.ColumnName}' has the type '{colFromDB.DataType}' instead of '{colFromStruct.DataType}', in the table {DBCommandsConsts.DbScriptsExecutionHistoryFullTableName}";
                                return errorMsg;
                            }
                        }
                    }

                }


                DataTable scriptsExecutionHistoryFilesTableFromDB = systemTablesSetFromDB.Tables[DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName];

                using (DataTable scriptsExecutionHistoryFilesTableFromStructure = CreateScriptsExecutionHistoryFilesTableStructure())
                {
                    foreach (DataColumn colFromStruct in scriptsExecutionHistoryFilesTableFromStructure.Columns)
                    {
                        if (!scriptsExecutionHistoryFilesTableFromDB.Columns.Contains(colFromStruct.ColumnName))
                        {
                            string errorMsg = $"The table '{DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName}' is missing the column '{colFromStruct}'";
                            return errorMsg;
                        }
                        else
                        {
                            DataColumn colFromDB = scriptsExecutionHistoryFilesTableFromStructure.Columns[colFromStruct.ColumnName];
                            if (colFromDB.DataType != colFromStruct.DataType)
                            {
                                string errorMsg = $"The column '{colFromStruct.ColumnName}' has the type '{colFromDB.DataType}' instead of '{colFromStruct.DataType}', in the table {DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName}";
                                return errorMsg;
                            }
                        }
                    }
                }

            }






            return "";
        }



        private static DataTable CreateScriptsExecutionHistoryTableStructure()
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

        private static DataTable CreateScriptsExecutionHistoryFilesTableStructure()
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
