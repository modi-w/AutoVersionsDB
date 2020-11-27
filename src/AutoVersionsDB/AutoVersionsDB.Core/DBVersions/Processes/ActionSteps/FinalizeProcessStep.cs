using AutoVersionsDB.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using System;
using System.Data;
using System.Globalization;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{
    public class FinalizeProcessStep : DBVersionsStep
    {
        public override string StepName => "Finalize Process";

        private readonly DBCommandsFactoryProvider _dbCommandsFactoryProvider;



        public FinalizeProcessStep(DBCommandsFactoryProvider dbCommandsFactoryProvider)
        {
            dbCommandsFactoryProvider.ThrowIfNull(nameof(dbCommandsFactoryProvider));

            _dbCommandsFactoryProvider = dbCommandsFactoryProvider;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(processContext.ProjectConfig.DBConnectionInfo).AsDisposable())
            {
                processContext.EndProcessDateTime = DateTime.Now;

                ScriptsExecution scriptsExecution = new ScriptsExecution();

                scriptsExecution.StartProcessDateTime = processContext.StartProcessDateTime.Value;
                scriptsExecution.ExecutionTypeName = processContext.ProcessDefinition.EngineTypeName;
                scriptsExecution.EndProcessDateTime = processContext.EndProcessDateTime.Value;
                scriptsExecution.ProcessDurationInMs = processContext.ProcessDurationInMs;
                scriptsExecution.NumOfScriptFiles = processContext.ExecutedFiles.Count;
                scriptsExecution.DBBackupFileFullPath = processContext.DBBackupFileFullPath;
                scriptsExecution.IsVirtualExecution = processContext.IsVirtualExecution;


                foreach (var executedFiles in processContext.ExecutedFiles)
                {
                    ScriptsExecutionFile scriptsExecutionFile = new ScriptsExecutionFile();

                    scriptsExecutionFile.ExecutedDateTime = DateTime.Now;
                    scriptsExecutionFile.Filename = executedFiles.Filename;
                    scriptsExecutionFile.FileFullPath = executedFiles.FileFullPath;
                    scriptsExecutionFile.ScriptFileType = executedFiles.ScriptFileType.FileTypeCode;
                    scriptsExecutionFile.IsVirtualExecution = processContext.IsVirtualExecution;
                    scriptsExecutionFile.ComputedFileHash = executedFiles.ComputedHash;
                    scriptsExecutionFile.ComputedFileHashDateTime = executedFiles.ComputedHashDateTime;


                    scriptsExecution.ScriptsExecutionFiles.Add(scriptsExecutionFile);
                }

                dbCommands.Instance.UpdateScriptsExecutionToDB(scriptsExecution);
            }
        }


    }





}
