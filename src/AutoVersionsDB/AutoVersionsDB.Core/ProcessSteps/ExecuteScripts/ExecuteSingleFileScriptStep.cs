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
        public override bool HasInternalStep => true;


        public ExecuteSingleFileScriptStep( ExecuteScriptBlockStepFactory executeScriptBlockStepFactory, IDBCommands dbCommands, string stepName, RuntimeScriptFileBase scriptFile)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _stepName = stepName;
            _scriptFile = scriptFile;

            _dbCommands = dbCommands;
            _executeScriptBlockStepFactory = executeScriptBlockStepFactory;
        }
       


        public override int GetNumOfInternalSteps(ProjectConfigItem projectConfig, AutoVersionsDbProcessState processState)
        {
            processState.ThrowIfNull(nameof(processState));


            string sqlCommandStr = File.ReadAllText(_scriptFile.FileFullPath);

            int numOfScriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).Count();

            return numOfScriptBlocks;
        }

        public override void Execute(ProjectConfigItem projectConfig, NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState)
        {
            projectConfig.ThrowIfNull(nameof(projectConfig));
            notificationExecutersProvider.ThrowIfNull(nameof(notificationExecutersProvider));
            processState.ThrowIfNull(nameof(processState));

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            if (!isVirtualExecution)
            {
                string sqlCommandStr = File.ReadAllText(_scriptFile.FileFullPath);

                List<string> scriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).ToList();

                List<NotificationableActionStepBase> internalSteps = new List<NotificationableActionStepBase>();
                foreach (string scriptBlockStr in scriptBlocks)
                {
                    var executeScriptBlockStep = _executeScriptBlockStepFactory.Craete(_dbCommands, scriptBlockStr);
                    internalSteps.Add(executeScriptBlockStep);
                }

                using (NotificationWrapperExecuter notificationWrapperExecuter =
                        notificationExecutersProvider.CreateNotificationWrapperExecuter(this.StepName, internalSteps, false))
                {
                    notificationWrapperExecuter.Execute(projectConfig, notificationExecutersProvider, processState);
                }
            }

            processState.AppendExecutedFile(_scriptFile);

        }

        
    }
}
