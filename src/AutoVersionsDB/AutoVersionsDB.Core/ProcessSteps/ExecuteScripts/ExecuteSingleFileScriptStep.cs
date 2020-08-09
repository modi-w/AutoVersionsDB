﻿using AutoVersionsDB.Core.ConfigProjects;
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
        private string _stepName;
        public override string StepName => _stepName;
        public override bool HasInternalStep => true;

        private RuntimeScriptFileBase _scriptFile;


        private IDBCommands _dbCommands;
        private ExecuteScriptBlockStepFactory _executeScriptBlockStepFactory;

        public ExecuteSingleFileScriptStep( ExecuteScriptBlockStepFactory executeScriptBlockStepFactory, IDBCommands dbCommands, string stepName, RuntimeScriptFileBase scriptFile)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _stepName = stepName;
            _scriptFile = scriptFile;

            _dbCommands = dbCommands;
            _executeScriptBlockStepFactory = executeScriptBlockStepFactory;
        }
       


        public override int GetNumOfInternalSteps(ProjectConfig projectConfig, AutoVersionsDbProcessState processState)
        {
            processState.ThrowIfNull(nameof(processState));


            string sqlCommandStr = File.ReadAllText(_scriptFile.FileFullPath);

            int numOfScriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).Count();

            return numOfScriptBlocks;
        }

        public override void Execute(ProjectConfig projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            processState.ThrowIfNull(nameof(processState));

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            if (!isVirtualExecution)
            {
                string sqlCommandStr = File.ReadAllText(_scriptFile.FileFullPath);

                List<string> scriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).ToList();

                using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(scriptBlocks.Count))
                {
                    foreach (string scriptBlockStr in scriptBlocks)
                    {
                        if (!notificationExecutersProvider.NotifictionStatesHistory.HasError)
                        {
                          var executeScriptBlockStep = _executeScriptBlockStepFactory.Craete(_dbCommands, scriptBlockStr);

                            notificationWrapperExecuter.ExecuteStep(executeScriptBlockStep, projectConfig, processState);
                        }
                    }
                }
            }

            processState.AppendExecutedFile(_scriptFile);

        }

        
    }
}
