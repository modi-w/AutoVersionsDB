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
    public class CreateVirtualExecutionsEngine : AutoVersionsDbEngineSettingBase
    {
        public override string EngineTypeName => "Create Virtual Executions";


        public CreateVirtualExecutionsEngine(RestoreDatabaseStep rollbackStep,
                                                ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                CreateScriptFilesStateStep createScriptFilesStateStep,
                                                CreateBackupStep createBackupStep,
                                                RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                                ExecuteAllScriptsStep executeScriptsStep,
                                                FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep)
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
