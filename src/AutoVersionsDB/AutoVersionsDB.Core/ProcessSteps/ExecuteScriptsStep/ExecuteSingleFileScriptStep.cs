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
        private ExecuteScriptBlockStep _executeScriptBlockStep;


        private IDBCommands _dbCommands;

        private bool _isVirtualExecution;


        public ExecuteSingleFileScriptStep(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                ExecuteScriptBlockStep executeScriptBlockStep,
                                                IDBCommands dbCommands,
                                                bool isVirtualExecution)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
            _executeScriptBlockStep = executeScriptBlockStep;
            _dbCommands = dbCommands;

            _isVirtualExecution = isVirtualExecution;

        }


        public override int GetNumOfInternalSteps(AutoVersionsDbProcessState processState, ScriptFileInfoStepArgs actionStepArgs)
        {
            string currentScriptFilename = actionStepArgs.ScriptFile.Filename;

            string sqlCommandStr = File.ReadAllText(actionStepArgs.ScriptFile.FileFullPath);

            int numOfScriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).Count();

            return numOfScriptBlocks;
        }


        public override void Execute(AutoVersionsDbProcessState processState, ScriptFileInfoStepArgs actionStepArgs)
        {
            if (!_isVirtualExecution)
            {
                string currentScriptFilename = actionStepArgs.ScriptFile.Filename;

                _stepName = currentScriptFilename;

                string sqlCommandStr = File.ReadAllText(actionStepArgs.ScriptFile.FileFullPath);

                List<string> scriptBlocks = _dbCommands.SplitSqlStatementsToExecutionBlocks(sqlCommandStr).ToList();

                using (NotificationWrapperExecuter notificationWrapperExecuter = _notificationExecutersFactoryManager.CreateNotificationWrapperExecuter(scriptBlocks.Count))
                {
                    foreach (string scriptBlockStr in scriptBlocks)
                    {
                        if (!_notificationExecutersFactoryManager.HasError)
                        {
                            ScriptBlockStepArgs scriptBlockStepArgs = new ScriptBlockStepArgs(scriptBlockStr);

                            notificationWrapperExecuter.ExecuteStep(_executeScriptBlockStep, null, processState, scriptBlockStepArgs);
                        }
                    }
                }
            }

            processState.AppendExecutedFile(actionStepArgs.ScriptFile);

        }


    }
}
