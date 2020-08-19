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
    public class RecreateDBFromScratchEngine : AutoVersionsDbEngine
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
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(checkDeliveryEnvValidationStep);
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(resetDBStep);
            ProcessSteps.Add(recreateDBVersionsTablesStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);
        }
    }
}
