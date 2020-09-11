using AutoVersionsDB.Core.ConfigProjects;
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
    public class SyncDBProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Sync DB";


        public SyncDBProcessDefinition(RestoreDatabaseStep rollbackStep,
                                        ValidationsStep<ProjectCodeExistValidationsFactory> projectCodeExistValidationStep,
                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                        ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                        ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep,
                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                        ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                        ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                                        CreateBackupStep createBackupStep,
                                        ExecuteAllScriptsStep executeScriptsStep,
                                        FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep, projectCodeExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(artifactFileValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
            AddStep(dbStateValidationStep);
            AddStep(createBackupStep);
            AddStep(executeScriptsStep);
            AddStep(finalizeProcessStep);
        }



    }
}
