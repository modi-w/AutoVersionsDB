﻿using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.DBVersions.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts
{
    public class ExecuteSingleFileScriptStep : DBVersionsStep
    {
        private readonly RuntimeScriptFileBase _scriptFile;
        private readonly DBCommands _dbCommands;
        private readonly ExecuteScriptBlockStepFactory _executeScriptBlockStepFactory;


        private readonly string _stepName;
        public override string StepName => _stepName;


        public ExecuteSingleFileScriptStep(ExecuteScriptBlockStepFactory executeScriptBlockStepFactory, DBCommands dbCommands, string stepName, RuntimeScriptFileBase scriptFile)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _stepName = stepName;
            _scriptFile = scriptFile;

            _dbCommands = dbCommands;
            _executeScriptBlockStepFactory = executeScriptBlockStepFactory;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            if (!processContext.IsVirtualExecution)
            {
                string sqlCommandStr = File.ReadAllText(_scriptFile.FileFullPath);

                List<string> scriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).ToList();

                List<ActionStepBase> internalSteps = new List<ActionStepBase>();

                foreach (string scriptBlockStr in scriptBlocks)
                {
                    var executeScriptBlockStep = _executeScriptBlockStepFactory.Craete(_dbCommands, scriptBlockStr);
                    internalSteps.Add(executeScriptBlockStep);
                }

                ExecuteInternalSteps(internalSteps, false);
            }

            processContext.AppendExecutedFile(_scriptFile);

        }


    }
}
