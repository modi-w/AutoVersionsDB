using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using Ninject;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScripts
{
    public static class ExecuteScriptsStepFluent
    {
        public static AutoVersionsDbEngine ExecuteScripts(this AutoVersionsDbEngine autoVersionsDbEngine, IDBCommands dbCommands, bool isVirtualExecution, ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            autoVersionsDbEngine.ThrowIfNull(nameof(autoVersionsDbEngine));
            dbCommands.ThrowIfNull(nameof(dbCommands));

            ExecuteScriptsStepFactory ExecuteScriptsStepFactory = NinjectUtils.KernelInstance.Get<ExecuteScriptsStepFactory>();

            ExecuteScriptsStep projectConfigValidationStep = 
                ExecuteScriptsStepFactory.Create(dbCommands, isVirtualExecution, scriptFilesComparersProvider);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }

    public class ExecuteScriptsStepFactory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;


        public ExecuteScriptsStepFactory(NotificationExecutersFactoryManager notificationExecutersFactoryManager)
        {
            _notificationExecutersFactoryManager = notificationExecutersFactoryManager;
        }

        public ExecuteScriptsStep Create(IDBCommands dbCommands, bool isVirtualExecution, ScriptFilesComparersProvider scriptFilesComparersProvider)
        {

            ExecuteScriptBlockStep executeScriptBlockStep = new ExecuteScriptBlockStep(dbCommands, isVirtualExecution);

            ExecuteSingleFileScriptStep runSingleFileScriptStep =
                new ExecuteSingleFileScriptStep(_notificationExecutersFactoryManager, executeScriptBlockStep, dbCommands, isVirtualExecution);

            ExecuteScriptsStep runScriptFilesScriptsStep =
                new ExecuteScriptsStep(_notificationExecutersFactoryManager,
                                                scriptFilesComparersProvider,
                                                runSingleFileScriptStep,
                                                isVirtualExecution);

            return runScriptFilesScriptsStep;
        }
    }
}
