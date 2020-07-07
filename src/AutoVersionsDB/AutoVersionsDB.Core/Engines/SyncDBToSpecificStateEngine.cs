using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ExecuteScripts;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class SyncDBToSpecificStateEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Set DB To Specific State";


        public SyncDBToSpecificStateEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                            RestoreDatabaseStep rollbackStep,
                                            ProjectConfigValidationStep projectConfigValidationStep,
                                            CheckDeliveryEnvValidationStep checkDeliveryEnvValidationStep,
                                            SystemTableValidationStep systemTableValidationStep,
                                            DBStateValidationStep dbStateValidationStep,
                                            TargetStateScriptFileValidationStep targetStateScriptFileValidationStep,
                                            CreateBackupStep createBackupStep,
                                            ExecuteScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(notificationExecutersFactoryManager, rollbackStep)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(checkDeliveryEnvValidationStep);
            ProcessSteps.Add(systemTableValidationStep);
            ProcessSteps.Add(dbStateValidationStep);
            ProcessSteps.Add(targetStateScriptFileValidationStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);
        }
    }
}
