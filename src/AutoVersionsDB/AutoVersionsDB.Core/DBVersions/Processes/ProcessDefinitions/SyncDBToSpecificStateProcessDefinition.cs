using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.NotificationableEngine.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class SyncDBToSpecificStateProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Set DB To Specific State";


        public SyncDBToSpecificStateProcessDefinition(RestoreDatabaseStep rollbackStep,
                                            ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
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
            : base(rollbackStep, idExistValidationStep, setProjectConfigInProcessContextStep)
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
