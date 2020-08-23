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
    public class RecreateDBFromScratchEngine : AutoVersionsDbEngineSettingBase
    {
        public override string EngineTypeName => "Recreate DB From Scratch";


        public RecreateDBFromScratchEngine(RestoreDatabaseStep rollbackStep,
                                            ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                            ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                            CreateScriptFilesStateStep createScriptFilesStateStep,
                                            CreateBackupStep createBackupStep,
                                            ResetDBStep resetDBStep,
                                            RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                            ExecuteAllScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(createBackupStep);
            AddStep(resetDBStep);
            AddStep(recreateDBVersionsTablesStep);
            AddStep(executeScriptsStep);
            AddStep(finalizeProcessStep);
        }
    }
}
