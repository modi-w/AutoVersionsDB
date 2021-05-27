using AutoVersionsDB.DB;
using AutoVersionsDB.DB.Contract;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Data;

namespace AutoVersionsDB.Core.DBVersions.Processes.Validators
{
    public class SystemTablesValidator : ValidatorBase
    {
        private readonly DBCommandsFactory _dbCommandsFactory;
        private readonly bool _isDevEnvironment;
        private readonly DBConnectionInfo _dbConnectionInfo;

        public const string Name = "SystemTables";
        public override string ValidatorName => Name;


        public override NotificationErrorType NotificationErrorType => NotificationErrorType.Error; 

        public override string ErrorInstructionsMessage
        {
            get
            {
                if (_isDevEnvironment)
                {
                    return CoreTextResources.SystemTablesDevEnvInstructionsMessage;
                }
                else
                {
                    return CoreTextResources.SystemTablesDeliveryEnvInstructionsMessage;
                }
            }
        }



        public SystemTablesValidator(DBCommandsFactory dbCommandsFactory,
                                        bool isDevEnvironment,
                                        DBConnectionInfo dbConnectionInfo)
        {
            _dbCommandsFactory = dbCommandsFactory;
            _isDevEnvironment = isDevEnvironment;
            _dbConnectionInfo = dbConnectionInfo;
        }

        public override string Validate()
        {
            using (var dbCommands = _dbCommandsFactory.CreateDBCommand(_dbConnectionInfo))
            {
                if (!dbCommands.CheckIfTableExist(DBCommandsConsts.DBSchemaName, DBCommandsConsts.DBScriptsExecutionHistoryTableName))
                {
                    return CoreTextResources.TableNotExistErrorMessage.Replace("[TableName]", DBCommandsConsts.DBScriptsExecutionHistoryFullTableName);
                }

                if (!dbCommands.CheckIfTableExist(DBCommandsConsts.DBSchemaName, DBCommandsConsts.DBScriptsExecutionHistoryFilesTableName))
                {
                    return CoreTextResources.TableNotExistErrorMessage.Replace("[TableName]", DBCommandsConsts.DBScriptsExecutionHistoryFilesTableName);
                }


                DataSet systemTablesSetFromDB = dbCommands.GetScriptsExecutionHistoryTableStructureFromDB();

                DataTable scriptsExecutionHistoryTableFromDB = systemTablesSetFromDB.Tables[DBCommandsConsts.DBScriptsExecutionHistoryFullTableName];

                using (DataTable scriptsExecutionHistoryTableFromStructure = CreateScriptsExecutionHistoryTableStructure())
                {
                    foreach (DataColumn colFromStruct in scriptsExecutionHistoryTableFromStructure.Columns)
                    {
                        if (!scriptsExecutionHistoryTableFromDB.Columns.Contains(colFromStruct.ColumnName))
                        {
                            return CoreTextResources
                                .TableMissingColumnErrorMessage
                                .Replace("[TableName]", DBCommandsConsts.DBScriptsExecutionHistoryFullTableName)
                                .Replace("[ColumnName]", colFromStruct.ColumnName);

                        }
                        else
                        {
                            DataColumn colFromDB = scriptsExecutionHistoryTableFromDB.Columns[colFromStruct.ColumnName];
                            if (colFromDB.DataType != colFromStruct.DataType)
                            {
                                return CoreTextResources
                                    .TableColumnIvalidTypeErrorMessage
                                    .Replace("[ColumnName]", colFromStruct.ColumnName)
                                    .Replace("[DBDataType]", colFromDB.DataType.Name)
                                    .Replace("[StructDataType]", colFromStruct.DataType.Name)
                                    .Replace("[TableName]", DBCommandsConsts.DBScriptsExecutionHistoryFullTableName);
                            }
                        }
                    }

                }


                DataTable scriptsExecutionHistoryFilesTableFromDB = systemTablesSetFromDB.Tables[DBCommandsConsts.DBScriptsExecutionHistoryFilesFullTableName];

                using (DataTable scriptsExecutionHistoryFilesTableFromStructure = CreateScriptsExecutionHistoryFilesTableStructure())
                {
                    foreach (DataColumn colFromStruct in scriptsExecutionHistoryFilesTableFromStructure.Columns)
                    {
                        if (!scriptsExecutionHistoryFilesTableFromDB.Columns.Contains(colFromStruct.ColumnName))
                        {
                            return CoreTextResources
                               .TableMissingColumnErrorMessage
                               .Replace("[TableName]", DBCommandsConsts.DBScriptsExecutionHistoryFilesFullTableName)
                               .Replace("[ColumnName]", colFromStruct.ColumnName);
                        }
                        else
                        {
                            DataColumn colFromDB = scriptsExecutionHistoryFilesTableFromStructure.Columns[colFromStruct.ColumnName];
                            if (colFromDB.DataType != colFromStruct.DataType)
                            {
                                return CoreTextResources
                                   .TableColumnIvalidTypeErrorMessage
                                   .Replace("[ColumnName]", colFromStruct.ColumnName)
                                   .Replace("[DBDataType]", colFromDB.DataType.Name)
                                   .Replace("[StructDataType]", colFromStruct.DataType.Name)
                                   .Replace("[TableName]", DBCommandsConsts.DBScriptsExecutionHistoryFilesFullTableName);
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
