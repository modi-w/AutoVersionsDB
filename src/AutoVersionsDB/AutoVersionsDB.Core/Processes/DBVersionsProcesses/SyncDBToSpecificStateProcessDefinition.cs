using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ExecuteScripts;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Processes.DBVersionsProcesses
{
    public class SyncDBToSpecificStateProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Set DB To Specific State";


        public SyncDBToSpecificStateProcessDefinition(RestoreDatabaseStep rollbackStep,
                                            ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                            SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                            ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                            ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                            CreateScriptFilesStateStep createScriptFilesStateStep,
                                            ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                            ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                                            ValidationsStep<TargetStateScriptFileValidationsFactory> targetStateScriptFileValidationStep,
                                            CreateBackupStep createBackupStep,
                                            ExecuteAllScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
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
