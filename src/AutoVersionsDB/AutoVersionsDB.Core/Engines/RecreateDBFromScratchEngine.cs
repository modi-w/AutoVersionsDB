using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ExecuteScripts;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class RecreateDBFromScratchEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Recreate DB From Scratch";


        public RecreateDBFromScratchEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            RestoreDatabaseStep rollbackStep,
                                            ProjectConfigValidationStep projectConfigValidationStep,
                                            CheckDeliveryEnvValidationStep checkDeliveryEnvValidationStep,
                                            CreateBackupStep createBackupStep,
                                            ResetDBStep resetDBStep,
                                            RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                            ExecuteScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(notificationExecutersFactoryManager, rollbackStep)
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
