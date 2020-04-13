using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScriptsStep
{
    public class ExecuteSingleFileScriptStep : NotificationableActionStepBase<AutoVersionsDbProcessState, ScriptFileInfoStepArgs>
    {
        private string _stepName;
        public override string StepName => _stepName;

        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;


        private IDBCommands _dbCommands;

        private bool _isVirtualExecution;


        public ExecuteSingleFileScriptStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                ExecuteScriptBlockStep executeScriptBlockStep,
                                                IDBCommands dbCommands,
                                                bool isVirtualExecution)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            InternalNotificationableAction = executeScriptBlockStep;
            _dbCommands = dbCommands;

            _isVirtualExecution = isVirtualExecution;

        }


        public void OverrideStepName(string stepName)
        {
            _stepName = stepName;
        }

        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ScriptFileInfoStepArgs actionStepArgs)
        {
            string sqlCommandStr = File.ReadAllText(actionStepArgs.ScriptFile.FileFullPath);

            int numOfScriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).Count();

            return numOfScriptBlocks;
        }

        public override void Execute(AutoVersionsDbProcessState processState, ScriptFileInfoStepArgs actionStepArgs)
        {
            if (!_isVirtualExecution)
            {
                string sqlCommandStr = File.ReadAllText(actionStepArgs.ScriptFile.FileFullPath);

                List<string> scriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).ToList();

                using (NotificationWrapperExecuter notificationWrapperExecuter = _notificationExecutersFactoryManager.CreateNotificationWrapperExecuter(scriptBlocks.Count))
                {
                    foreach (string scriptBlockStr in scriptBlocks)
                    {
                        if (!_notificationExecutersFactoryManager.HasError)
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
