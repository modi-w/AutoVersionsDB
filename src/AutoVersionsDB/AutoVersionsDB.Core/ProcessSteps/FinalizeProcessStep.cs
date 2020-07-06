using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Data;
using System.Globalization;
using System.Linq;

namespace AutoVersionsDB.Core.ProcessSteps
{
          

    public class FinalizeProcessStep : AutoVersionsDbStep, IDisposable
    {
        public override string StepName => "Finalize Process";

        private DBCommandsFactoryProvider _dbCommandsFactoryProvider;
        private IDBCommands _dbCommands;



        public FinalizeProcessStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }

        public override void Prepare(ProjectConfigItem projectConfig)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));

            _dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, projectConfig.DBCommandsTimeout);

        }

        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            return 1;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ActionStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));

            DataSet dsExecutionHistory = _dbCommands.GetScriptsExecutionHistoryTableStructureFromDB();

            DataTable dbScriptsExecutionHistoryTable = dsExecutionHistory.Tables[DBCommandsConsts.DbScriptsExecutionHistoryFullTableName];
            DataTable dbScriptsExecutionHistoryFilesTable = dsExecutionHistory.Tables[DBCommandsConsts.DbScriptsExecutionHistoryFilesFullTableName];

            processState.EndProcessDateTime = DateTime.Now;

            DataRow executionHistoryRow = dbScriptsExecutionHistoryTable.NewRow();

            executionHistoryRow["DBScriptsExecutionHistoryID"] = 0;

            executionHistoryRow["StartProcessDateTime"] = processState.StartProcessDateTime;
            executionHistoryRow["ExecutionTypeName"] = processState.EngineMetaData["EngineTypeName"];
            executionHistoryRow["EndProcessDateTime"] = processState.EndProcessDateTime;
            executionHistoryRow["ProcessDurationInMs"] = processState.ProcessDurationInMs;
            executionHistoryRow["NumOfScriptFiles"] = processState.ExecutedFiles.Count;
            executionHistoryRow["DBBackupFileFullPath"] = processState.DBBackupFileFullPath;
            executionHistoryRow["IsVirtualExecution"] = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

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
                newFileRow["IsVirtualExecution"] = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture); 
                newFileRow["ComputedFileHash"] = executedFiles.ComputedHash;
                newFileRow["ComputedFileHashDateTime"] = executedFiles.ComputedHashDateTime;




                dbScriptsExecutionHistoryFilesTable.Rows.Add(newFileRow);
            }


            int currDBScriptsExecutionHistoryID = Convert.ToInt32(executionHistoryRow["DBScriptsExecutionHistoryID"], CultureInfo.InvariantCulture);

            foreach (DataRow fileRow in dbScriptsExecutionHistoryFilesTable.Rows)
            {
                fileRow["DBScriptsExecutionHistoryID"] = currDBScriptsExecutionHistoryID;
            }

            _dbCommands.UpdateScriptsExecutionHistoryFilesTableToDB(dbScriptsExecutionHistoryFilesTable);


        }


        #region IDisposable

        private bool _disposed = false;

        ~FinalizeProcessStep() => Dispose(false);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                if (_dbCommands != null)
                {
                    _dbCommands.Dispose();
                }
            }

            _disposed = true;
        }

        #endregion

    }





}
