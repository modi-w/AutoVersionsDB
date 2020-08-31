using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
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



        public override void Execute(AutoVersionsDbProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));


            string targetStateScriptFileName = null;
            if (processContext.ProcessParams != null)
            {
                targetStateScriptFileName = (processContext.ProcessParams as AutoVersionsDbProcessParams).TargetStateScriptFileName;
            }

            ScriptFilesComparerBase scriptFilesComparer = processContext.ScriptFilesState.GetScriptFilesComparerByType(_fileTypeCode);

            List<RuntimeScriptFileBase> scriptFilesList = scriptFilesComparer.GetPendingFilesToExecute(targetStateScriptFileName);

            foreach (RuntimeScriptFileBase scriptFile in scriptFilesList)
            {
                string ignoreStr = "";
                if (processContext.IsVirtualExecution)
                {
                    ignoreStr = " - Ignore (virtual execution)";
                }

                string stepName = $"{scriptFile.Filename}{ignoreStr}";

                ExecuteSingleFileScriptStep executeSingleFileScriptStep = _executeSingleFileScriptStepFactory.Create(_dbCommands, stepName, scriptFile);

                AddInternalStep(executeSingleFileScriptStep);
            }

            ExecuteInternalSteps( false);

        }


    }

  
}
