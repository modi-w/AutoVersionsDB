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
    public class CreateVirtualExecutionsEngine : AutoVersionsDbScriptsEngine
    {
        public override string EngineTypeName => "Create Virtual Executions";


        public CreateVirtualExecutionsEngine(NotificationExecutersFactoryManager notificationExecutersFactoryManager,
                                                RestoreDatabaseStep rollbackStep,
                                                ScriptFilesComparersManager scriptFilesComparersManager,
                                                ProjectConfigValidationStep projectConfigValidationStep,
                                                CreateBackupStep createBackupStep,
                                                RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                                ExecuteScriptsStep executeScriptsStep,
                                                FinalizeProcessStep finalizeProcessStep)
            : base(notificationExecutersFactoryManager, rollbackStep, scriptFilesComparersManager)
        {
            ProcessSteps.Add(projectConfigValidationStep);
            ProcessSteps.Add(createBackupStep);
            ProcessSteps.Add(recreateDBVersionsTablesStep);
            ProcessSteps.Add(executeScriptsStep);
            ProcessSteps.Add(finalizeProcessStep);

            IsVirtualExecution = true;
        }
    }
}
