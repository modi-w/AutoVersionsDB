using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ExecuteScripts;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class RecreateDBFromScratchProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Recreate DB From Scratch";


        public RecreateDBFromScratchProcessDefinition(RestoreDatabaseStep rollbackStep,
                                            ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                            SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                            ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                            ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                            CreateScriptFilesStateStep createScriptFilesStateStep,
                                            CreateBackupStep createBackupStep,
                                            ResetDBStep resetDBStep,
                                            RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                            ExecuteAllScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
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
