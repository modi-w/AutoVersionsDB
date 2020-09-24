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
    public class CreateVirtualExecutionsProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Create Virtual Executions";


        public CreateVirtualExecutionsProcessDefinition(RestoreDatabaseStep rollbackStep,
                                                ValidationsStep<ProjectCodeExistDBVersionsValidationsFactory> projectCodeExistValidationStep,
                                                SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                CreateScriptFilesStateStep createScriptFilesStateStep,
                                                CreateBackupStep createBackupStep,
                                                RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                                ExecuteAllScriptsStep executeScriptsStep,
                                                FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(createBackupStep);
            AddStep(recreateDBVersionsTablesStep);
            AddStep(executeScriptsStep);
            AddStep(finalizeProcessStep);

            IsVirtualExecution = true;
        }
    }
}
