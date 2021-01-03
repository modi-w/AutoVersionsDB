using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.DB;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using System.Collections.Generic;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteScriptsByTypeStep : DBVersionsStep
    {
        private readonly DBCommands _dbCommands;
        private readonly ExecuteSingleFileScriptStepFactory _executeSingleFileScriptStepFactory;
        private readonly string _fileTypeCode;
        public override string StepName => _fileTypeCode;


        public ExecuteScriptsByTypeStep(string fileTypeCode, DBCommands dbCommands, ExecuteSingleFileScriptStepFactory executeSingleFileScriptStepFactory)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));
            executeSingleFileScriptStepFactory.ThrowIfNull(nameof(executeSingleFileScriptStepFactory));

            _fileTypeCode = fileTypeCode;
            _dbCommands = dbCommands;
            _executeSingleFileScriptStepFactory = executeSingleFileScriptStepFactory;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            string targetStateScriptFileName = null;
            if (processContext.ProcessArgs != null)
            {
                targetStateScriptFileName = (processContext.ProcessArgs as DBVersionsProcessArgs).TargetStateScriptFileName;
            }

            ScriptFilesComparerBase scriptFilesComparer = processContext.ScriptFilesState.GetScriptFilesComparerByType(_fileTypeCode);

            List<RuntimeScriptFileBase> scriptFilesList = scriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);

            List<ActionStepBase> internalSteps = new List<ActionStepBase>();

            foreach (RuntimeScriptFileBase scriptFile in scriptFilesList)
            {
                string ignoreStr = "";
                if (processContext.IsVirtualExecution)
                {
                    ignoreStr = " - Ignore (virtual execution)";
                }

                string stepName = $"{scriptFile.Filename}{ignoreStr}";

                ExecuteSingleFileScriptStep executeSingleFileScriptStep = _executeSingleFileScriptStepFactory.Create(_dbCommands, stepName, scriptFile);

                internalSteps.Add(executeSingleFileScriptStep);
            }

            ExecuteInternalSteps(internalSteps, false);

        }


    }


}
