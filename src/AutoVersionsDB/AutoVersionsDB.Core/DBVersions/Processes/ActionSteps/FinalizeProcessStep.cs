﻿using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;
using System;
using System.Data;
using System.Globalization;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{
    public class FinalizeProcessStep : DBVersionsStep
    {
        public override string StepName => "Finalize Process";

        private readonly DBCommandsFactory dbCommandsFactoryProvider;



        public FinalizeProcessStep(DBCommandsFactory dbCommandsFactory)
        {
            dbCommandsFactory.ThrowIfNull(nameof(dbCommandsFactory));

            dbCommandsFactoryProvider = dbCommandsFactory;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            using (var dbCommands = dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo))
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
