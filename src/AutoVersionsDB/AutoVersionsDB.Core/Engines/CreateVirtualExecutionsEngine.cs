using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.Core.ProcessSteps.ExecuteScripts;
using AutoVersionsDB.Core.ProcessSteps.Validations;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.Engines
{
    public class CreateVirtualExecutionsEngine : AutoVersionsDbEngine
    {
        public override string EngineTypeName => "Create Virtual Executions";


        public CreateVirtualExecutionsEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                RestoreDatabaseStep rollbackStep,
                                                ProjectConfigValidationStep projectConfigValidationStep,
                                                CreateBackupStep createBackupStep,
                                                ExecuteScriptsStep executeScriptsStep,
                                                FinalizeProcessStep finalizeProcessStep)
            : base(notificationExecutersFactoryManager, rollbackStep)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);

            IsVirtualExecution = true;
        }
    }
}
