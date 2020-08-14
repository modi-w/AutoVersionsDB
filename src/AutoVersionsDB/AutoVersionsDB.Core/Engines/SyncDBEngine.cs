using AutoVersionsDB.Core.ConfigProjects;
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
    public class SyncDBEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Sync DB";


        public SyncDBEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                            RestoreDatabaseStep rollbackStep,
                            ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                            ValidationsStep<ArtifactFileValidationsFactory> artifactFileValidationStep,
                            CreateScriptFilesStateStep createScriptFilesStateStep,
                            ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                            ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                            CreateBackupStep createBackupStep,
                            ExecuteScriptsStep executeScriptsStep,
                            FinalizeProcessStep finalizeProcessStep)
            :base(notificationExecutersProviderFactory, rollbackStep)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(artifactFileValidationStep);
            ProcessSteps.Add(createScriptFilesStateStep);
            ProcessSteps.Add(systemTableValidationStep);
            ProcessSteps.Add(dbStateValidationStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);
        }


    
    }
}
