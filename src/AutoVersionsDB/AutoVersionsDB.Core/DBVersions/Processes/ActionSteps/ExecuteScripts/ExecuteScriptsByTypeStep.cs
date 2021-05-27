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


            TargetScripts targetScripts = (processContext.ProcessArgs as DBVersionsProcessArgs).TargetScripts;
            //if (processContext.ProcessArgs != null)
            //{
            //    targetScripts = (processContext.ProcessArgs as DBVersionsProcessArgs).TargetScripts;
            //}

            ScriptFilesComparerBase scriptFilesComparer = processContext.ScriptFilesState.GetScriptFilesComparerByType(_fileTypeCode);

            if (!targetScripts.TargetScriptsByType.TryGetValue(_fileTypeCode, out string targetStateScriptFileName))
            {
                targetStateScriptFileName = null;
            }
            List<RuntimeScriptFile> scriptFilesList = scriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);

            List<ActionStepBase> internalSteps = new List<ActionStepBase>();

            foreach (RuntimeScriptFile scriptFile in scriptFilesList)
            {
                string ignoreStr = "";
                if (processContext.IsVirtualExecution)
                {
                    ignoreStr = CoreTextResources.IgnoreBecauseVirtualExecution;
                }

                string stepName = $"{scriptFile.Filename}{ignoreStr}";

                ExecuteSingleFileScriptStep executeSingleFileScriptStep = _executeSingleFileScriptStepFactory.Create(_dbCommands, stepName, scriptFile);

                internalSteps.Add(executeSingleFileScriptStep);
            }

            ExecuteInternalSteps(internalSteps, false);

        }


    }


}
