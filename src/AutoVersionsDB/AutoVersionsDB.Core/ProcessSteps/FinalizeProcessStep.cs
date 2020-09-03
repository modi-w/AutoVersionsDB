using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ProcessDefinitions;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using System;
using System.Data;
using System.Globalization;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public class FinalizeProcessStep : AutoVersionsDbStep
    {
        public override string StepName => "Finalize Process";

        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;



        public FinalizeProcessStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }



        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBTypeCode, processContext.ProjectConfig.ConnStr, processContext.ProjectConfig.DBCommandsTimeout))
            {
                DataSet dsExecutionHistory = dbCommands.GetScriptsExecutionHistoryTableStructureFromDB();

                DataTable dbScriptsExecutionHistoryTable = dsExecutionHistory.Tables[DBCommandsConsts.DbScriptsExecutionHistoryFullTableName];
                DataTable dbScriptsExecutionHistoryFilesTable = dsExecutionHistory.Tables[DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName];

                processContext.EndProcessDateTime = DateTime.Now;

                DataRow executionHistoryRow = dbScriptsExecutionHistoryTable.NewRow();

                executionHistoryRow["DBScriptsExecutionHistoryID"] = 0;

                executionHistoryRow["StartProcessDateTime"] = processContext.StartProcessDateTime;
                executionHistoryRow["ExecutionTypeName"] = processContext.ProcessDefinition.EngineTypeName;
                executionHistoryRow["EndProcessDateTime"] = processContext.EndProcessDateTime;
                executionHistoryRow["ProcessDurationInMs"] = processContext.ProcessDurationInMs;
                executionHistoryRow["NumOfScriptFiles"] = processContext.ExecutedFiles.Count;
                executionHistoryRow["DBBackupFileFullPath"] = processContext.DBBackupFileFullPath;
                executionHistoryRow["IsVirtualExecution"] = processContext.IsVirtualExecution;

                dbScriptsExecutionHistoryTable.Rows.Add(executionHistoryRow);
                dbCommands.UpdateScriptsExecutionHistoryTableToDB(dbScriptsExecutionHistoryTable);


                foreach (var executedFiles in processContext.ExecutedFiles)
                {
                    DataRow newFileRow = dbScriptsExecutionHistoryFilesTable.NewRow();

                    newFileRow["DBScriptsExecutionHistoryID"] = 0;
                    newFileRow["ExecutedDateTime"] = DateTime.Now;
                    newFileRow["Filename"] = executedFiles.Filename;
                    newFileRow["FileFullPath"] = executedFiles.FileFullPath;
                    newFileRow["ScriptFileType"] = executedFiles.ScriptFileType.FileTypeCode;
                    newFileRow["IsVirtualExecution"] = processContext.IsVirtualExecution;
                    newFileRow["ComputedFileHash"] = executedFiles.ComputedHash;
                    newFileRow["ComputedFileHashDateTime"] = executedFiles.ComputedHashDateTime;


                    dbScriptsExecutionHistoryFilesTable.Rows.Add(newFileRow);
                }


                int currDBScriptsExecutionHistoryID = Convert.ToInt32(executionHistoryRow["DBScriptsExecutionHistoryID"], CultureInfo.InvariantCulture);

                foreach (DataRow fileRow in dbScriptsExecutionHistoryFilesTable.Rows)
                {
                    fileRow["DBScriptsExecutionHistoryID"] = currDBScriptsExecutionHistoryID;
                }

                dbCommands.UpdateScriptsExecutionHistoryFilesTableToDB(dbScriptsExecutionHistoryFilesTable);

            }
        }


    }





}
