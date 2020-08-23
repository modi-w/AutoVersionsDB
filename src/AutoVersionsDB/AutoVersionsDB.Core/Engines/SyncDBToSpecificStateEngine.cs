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
    public class SyncDBToSpecificStateEngine : AutoVersionsDbEngineSettingBase
    {
        public override string EngineTypeName => "Set DB To Specific State";


        public SyncDBToSpecificStateEngine(RestoreDatabaseStep rollbackStep,
                                            ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                            ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                            CreateScriptFilesStateStep createScriptFilesStateStep,
                                            ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                            ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                                            ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep,
                                            CreateBackupStep createBackupStep,
                                            ExecuteAllScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
            AddStep(dbStateValidationStep);
            AddStep(targetStateScriptFileValidationStep);
            AddStep(createBackupStep);
            AddStep(executeScriptsStep);
            AddStep(finalizeProcessStep);
        }
    }
}
