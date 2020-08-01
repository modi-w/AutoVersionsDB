using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Engines;
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
    public class ExecuteSingleFileScriptStep : NotificationableActionStepBase<AutoVersionsDbProcessState, ProjectConfigItem, ScriptFileInfoStepArgs>
    {
        private string _stepName;
        public override string StepName => _stepName;

        private IDBCommands _dbCommands;


        public ExecuteSingleFileScriptStep(ExecuteScriptBlockStep executeScriptBlockStep)
        {
            InternalNotificationableAction = executeScriptBlockStep;
        }


        public void OverrideStepName(string stepName)
        {
            _stepName = stepName;
        }

        public override void Prepare(ProjectConfigItem projectConfig)
        {
        }

        public void SetDBCommands(IDBCommands dbCommands)
        {
            dbCommands.ThrowIfNull(nameof(dbCommands));

            _dbCommands = dbCommands;

            (InternalNotificationableAction as ExecuteScriptBlockStep).SetDBCommands(_dbCommands);
        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ScriptFileInfoStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));
            actionStepArgs.ThrowIfNull(nameof(actionStepArgs));


            string sqlCommandStr = File.ReadAllText(actionStepArgs.ScriptFile.FileFullPath);

            int numOfScriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).Count();

            return numOfScriptBlocks;
        }

        public override void Execute(NotificationExecutersProvider notificationExecutersProvider, AutoVersionsDbProcessState processState, ScriptFileInfoStepArgs actionStepArgs)
        {
            processState.ThrowIfNull(nameof(processState));
            actionStepArgs.ThrowIfNull(nameof(actionStepArgs));

            bool isVirtualExecution = Convert.ToBoolean(processState.EngineMetaData["IsVirtualExecution"], CultureInfo.InvariantCulture);

            if (!isVirtualExecution)
            {
                string sqlCommandStr = File.ReadAllText(actionStepArgs.ScriptFile.FileFullPath);

                List<string> scriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).ToList();

                using (NotificationWrapperExecuter notificationWrapperExecuter = notificationExecutersProvider.CreateNotificationWrapperExecuter(scriptBlocks.Count))
                {
                    foreach (string scriptBlockStr in scriptBlocks)
                    {
                        if (!notificationExecutersProvider.NotifictionStatesHistory.HasError)
                        {
                            ScriptBlockStepArgs scriptBlockStepArgs = new ScriptBlockStepArgs(scriptBlockStr);

                            notificationWrapperExecuter.ExecuteStep(InternalNotificationableAction, null, processState, scriptBlockStepArgs);
                        }
                    }
                }
            }

            processState.AppendExecutedFile(actionStepArgs.ScriptFile);

        }

        
    }
}
