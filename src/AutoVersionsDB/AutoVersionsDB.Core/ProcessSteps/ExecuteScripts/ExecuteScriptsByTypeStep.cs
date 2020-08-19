using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteScriptsByTypeStep : AutoVersionsDbStep
    {
        private readonly IDBCommands _dbCommands;
        private readonly ExecuteSingleFileScriptStepFactory _executeSingleFileScriptStepFactory;
        private readonly string _fileTypeCode;
        public override string StepName => _fileTypeCode;


        public ExecuteScriptsByTypeStep(string fileTypeCode, IDBCommands dbCommands, ExecuteSingleFileScriptStepFactory executeSingleFileScriptStepFactory)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));
            executeSingleFileScriptStepFactory.ThrowIfNull(nameof(executeSingleFileScriptStepFactory));

            _fileTypeCode= fileTypeCode;
            _dbCommands = dbCommands;
            _executeSingleFileScriptStepFactory = executeSingleFileScriptStepFactory;
        }



        public override void Execute(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            processState.ThrowIfNull(nameof(processState));


            string targetStateScriptFileName = null;
            if (processState.ExecutionParams != null)
            {
                targetStateScriptFileName = (processState.ExecutionParams as AutoVersionsDBExecutionParams).TargetStateScriptFileName;
            }

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);


            ScriptFilesComparerBase scriptFilesComparer = processState.ScriptFilesState.GetScriptFilesComparerByType(_fileTypeCode);

            List<RuntimeScriptFileBase> scriptFilesList = scriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);

            List<NotificationableActionStepBase> internalSteps = new List<NotificationableActionStepBase>();
            foreach (RuntimeScriptFileBase scriptFile in scriptFilesList)
            {
                string ignoreStr = "";
                if (isVirtualExecution)
                {
                    ignoreStr = " - Ignore (virtual execution)";
                }

                string stepName = $"{scriptFile.Filename}{ignoreStr}";

                ExecuteSingleFileScriptStep executeSingleFileScriptStep = _executeSingleFileScriptStepFactory.Create(_dbCommands, stepName, scriptFile);

                internalSteps.Add(executeSingleFileScriptStep);
            }

            onExecuteStepsList.Invoke(internalSteps, false);

        }


    }

  
}
