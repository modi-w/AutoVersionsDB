using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ExecuteScripts;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class CreateVirtualExecutionsEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Create Virtual Executions";


        public CreateVirtualExecutionsEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                                RestoreDatabaseStep rollbackStep,
                                                ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                CreateScriptFilesStateStep createScriptFilesStateStep,
                                                CreateBackupStep createBackupStep,
                                                RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                                ExecuteScriptsStep executeScriptsStep,
                                                FinalizeProcessStep finalizeProcessStep)
            : base(notificationExecutersProviderFactory, rollbackStep)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(recreateDBVersionsTablesStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);

            IsVirtualExecution = true;
        }
    }
}
