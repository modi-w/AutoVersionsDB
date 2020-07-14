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
    public class SyncDBEngine : AutoVersionsDbScriptsEngine
    {
        public override string EngineTypeName => "Sync DB";


        public SyncDBEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                            RestoreDatabaseStep rollbackStep,
                            ScriptFilesComparersManager scriptFilesComparersManager,
                            ProjectConfigValidationStep projectConfigValidationStep,
                            ArtifactFileValidationStep artifactFileValidationStep,
                            SystemTableValidationStep systemTableValidationStep,
                            DBStateValidationStep dbStateValidationStep,
                            CreateBackupStep createBackupStep,
                            ExecuteScriptsStep executeScriptsStep,
                            FinalizeProcessStep finalizeProcessStep)
            :base(notificationExecutersFactoryManager, rollbackStep, scriptFilesComparersManager)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(artifactFileValidationStep);
            ProcessSteps.Add(systemTableValidationStep);
            ProcessSteps.Add(dbStateValidationStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);
        }


    
    }
}
