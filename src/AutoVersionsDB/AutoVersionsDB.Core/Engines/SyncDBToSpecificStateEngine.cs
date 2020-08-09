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
    public class SyncDBToSpecificStateEngine : AutoVersionsDbScriptsEngine
    {
        public override string EngineTypeName => "Set DB To Specific State";


        public SyncDBToSpecificStateEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                            RestoreDatabaseStep rollbackStep,
                                            ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                            ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                            CreateScriptFilesStateStep createScriptFilesStateStep,
                                            ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                            ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                                            ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep,
                                            CreateBackupStep createBackupStep,
                                            ExecuteScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(notificationExecutersProviderFactory, rollbackStep)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(checkDeliveryEnvValidationStep);
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(systemTableValidationStep);
            ProcessSteps.Add(dbStateValidationStep);
            ProcessSteps.Add(targetStateScriptFileValidationStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);
        }
    }
}
