using AutoVersionsDB.Core.Engines;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.Utils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using Ninject;

namespace AutoVersionsDB.Core.ProcessSteps.ExecuteScriptsStep
{
    public static class ExecuteScriptsStepFluent
    {
        public static AutoVersionsDbEngine ExecuteScripts(this AutoVersionsDbEngine autoVersionsDbEngine, IDBCommands dbCommands, bool isVirtualExecution, ScriptFilesComparersProvider scriptFilesComparersProvider)
        {
            ExecuteScriptsStep_Factory ExecuteScriptsStepFactory = NinjectUtils.KernelInstance.Get<ExecuteScriptsStep_Factory>();

            ExecuteScriptsStep projectConfigValidationStep = 
                ExecuteScriptsStepFactory.Create(dbCommands, isVirtualExecution, scriptFilesComparersProvider);

            autoVersionsDbEngine.AppendProcessStep(projectConfigValidationStep);

            return autoVersionsDbEngine;
        }
    }

    public class ExecuteScriptsStep_Factory
    {
        private NotificationExecutersFactoryManager _notificationExecutersFactoryManager;


        public ExecuteScriptsStep_Factory(FileChecksumManager fileChecksumManager,
                                                    NotificationExecutersFactoryManager notificationExecutersFactoryManager)
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
