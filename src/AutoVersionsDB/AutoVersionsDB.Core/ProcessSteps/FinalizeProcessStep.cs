using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Data;
using System.Linq;

namespace AutoVersionsDB.Core.ProcessSteps
{
    public static class FinalizeProcessStepFluent
    {
        public static AutoVersionsDbEngine FinalizeProcess(this AutoVersionsDbEngine autoVersionsDbEngine,
                                                            IDBCommands dbCommands,
                                                            bool isVirtualExecution,
                                                            string executionTypeName)
        {
            FinalizeProcessStep finalizeProcessStep =
                new FinalizeProcessStep(dbCommands,
                                                    isVirtualExecution,
                                                    executionTypeName);


            autoVersionsDbEngine.AppendProcessStep(finalizeProcessStep);

            return autoVersionsDbEngine;
        }
    }


    public class FinalizeProcessStep : NotificationableActionStepBase<AutoVersionsDbProcessState>
    {
        public override string StepName => "Finalize Process";

        private IDBCommands _dbCommands;

        public bool IsVirtualExecution { get; private set; }
        public string ExecutionTypeName { get; private set; }


        public FinalizeProcessStep(IDBCommands dbCommands, 
                                            bool isVirtualExecution,
                                            string executionTypeName)
        {
            _dbCommands = dbCommands;
            IsVirtualExecution = isVirtualExecution;
            ExecutionTypeName = executionTypeName;
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            DataSet dsExecutionHistory = _dbCommands.GetScriptsExecutionHistoryTableStructureFromDB();

            DataTable dbScriptsExecutionHistoryTable = dsExecutionHistory.Tables[DBCommandsConsts.C_DBScriptsExecutionHistory_FullTableName];
            DataTable dbScriptsExecutionHistoryFilesTable = dsExecutionHistory.Tables[DBCommandsConsts.C_DBScriptsExecutionHistoryFiles_FullTableName];

            processState.EndProcessDateTime = DateTime.Now;

            DataRow executionHistoryRow = dbScriptsExecutionHistoryTable.NewRow();

            executionHistoryRow["DBScriptsExecutionHistoryID"] = 0;

            executionHistoryRow["StartProcessDateTime"] = processState.StartProcessDateTime;
            executionHistoryRow["ExecutionTypeName"] = ExecutionTypeName;
            executionHistoryRow["EndProcessDateTime"] = processState.EndProcessDateTime;
            executionHistoryRow["ProcessDurationInMs"] = processState.ProcessDurationInMs;
            executionHistoryRow["NumOfScriptFiles"] = processState.ExecutedFiles.Count;
            executionHistoryRow["DBBackupFileFullPath"] = processState.DBBackupFileFullPath;
            executionHistoryRow["IsVirtualExecution"] = IsVirtualExecution;

            dbScriptsExecutionHistoryTable.Rows.Add(executionHistoryRow);
            _dbCommands.UpdateScriptsExecutionHistoryTableToDB(dbScriptsExecutionHistoryTable);


            foreach (var executedFiles in processState.ExecutedFiles)
            {
                DataRow newFileRow = dbScriptsExecutionHistoryFilesTable.NewRow();

                newFileRow["DBScriptsExecutionHistoryID"] = 0;
                newFileRow["ExecutedDateTime"] = DateTime.Now;
                newFileRow["Filename"] = executedFiles.Filename;
                newFileRow["FileFullPath"] = executedFiles.FileFullPath;
                newFileRow["ScriptFileType"] = executedFiles.FileTypeCode;
                newFileRow["IsVirtualExecution"] = IsVirtualExecution;
                newFileRow["ComputedFileHash"] = executedFiles.ComputedHash;
                newFileRow["ComputedFileHashDateTime"] = executedFiles.ComputedHashDateTime;
                



                dbScriptsExecutionHistoryFilesTable.Rows.Add(newFileRow);
            }


            int currDBScriptsExecutionHistoryID = Convert.ToInt32(executionHistoryRow["DBScriptsExecutionHistoryID"]);

            foreach (DataRow fileRow in dbScriptsExecutionHistoryFilesTable.Rows)
            {
                fileRow["DBScriptsExecutionHistoryID"] = currDBScriptsExecutionHistoryID;
            }

            _dbCommands.UpdateScriptsExecutionHistoryFilesTableToDB(dbScriptsExecutionHistoryFilesTable);


        }



    }
}
