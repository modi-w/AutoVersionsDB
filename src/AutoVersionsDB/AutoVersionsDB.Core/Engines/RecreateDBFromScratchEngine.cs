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
    public class RecreateDBFromScratchEngine : AutoVersionsDbScriptsEngine
    {
        public override string EngineTypeName => "Recreate DB From Scratch";


        public RecreateDBFromScratchEngine(NotificationExecutersProviderFactory notificationExecutersProviderFactory,
                                            RestoreDatabaseStep rollbackStep,
                                            ScriptFilesComparersManager scriptFilesComparersManager,
                                            ProjectConfigValidationStep projectConfigValidationStep,
                                            CheckDeliveryEnvValidationStep checkDeliveryEnvValidationStep,
                                            CreateBackupStep createBackupStep,
                                            ResetDBStep resetDBStep,
                                            RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                            ExecuteScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(notificationExecutersProviderFactory, rollbackStep, scriptFilesComparersManager)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(checkDeliveryEnvValidationStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(resetDBStep);
            ProcessSteps.Add(recreateDBVersionsTablesStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);
        }
    }
}
