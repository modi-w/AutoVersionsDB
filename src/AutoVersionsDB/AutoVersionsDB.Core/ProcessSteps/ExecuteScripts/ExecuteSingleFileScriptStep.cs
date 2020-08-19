using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public class ExecuteSingleFileScriptStep : AutoVersionsDbStep
    {
        private readonly RuntimeScriptFileBase _scriptFile;
        private readonly IDBCommands _dbCommands;
        private readonly ExecuteScriptBlockStepFactory _executeScriptBlockStepFactory;


        private readonly string _stepName;
        public override string StepName => _stepName;


        public ExecuteSingleFileScriptStep(ExecuteScriptBlockStepFactory executeScriptBlockStepFactory, IDBCommands dbCommands, string stepName, RuntimeScriptFileBase scriptFile)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _stepName = stepName;
            _scriptFile = scriptFile;

            _dbCommands = dbCommands;
            _executeScriptBlockStepFactory = executeScriptBlockStepFactory;
        }



        public override void Execute(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState, Action<List<NotificationableActionStepBase>, bool> onExecuteStepsList)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            processState.ThrowIfNull(nameof(processState));

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            if (!isVirtualExecution)
            {
                string sqlCommandStr = File.ReadAllText(_scriptFile.FileFullPath);

                List<string> scriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).ToList();

                foreach (string scriptBlockStr in scriptBlocks)
                {
                    var executeScriptBlockStep = _executeScriptBlockStepFactory.Craete(_dbCommands, scriptBlockStr);
                    InternalSteps.Add(executeScriptBlockStep);
                }

                onExecuteStepsList.Invoke(InternalSteps, false);
            }

            processState.AppendExecutedFile(_scriptFile);

        }


    }
}
