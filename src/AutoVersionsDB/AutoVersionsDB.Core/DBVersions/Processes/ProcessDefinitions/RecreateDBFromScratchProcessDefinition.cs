using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class RecreateDBFromScratchProcessDefinition : DBVersionsProcessDefinition
    {
        public override string EngineTypeName => "Recreate DB From Scratch";


        public RecreateDBFromScratchProcessDefinition(RestoreDatabaseStep rollbackStep,
                                            ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                            SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                            ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                            ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                            CreateScriptFilesStateStep createScriptFilesStateStep,
                                            CreateBackupStep createBackupStep,
                                            ResetDBStep resetDBStep,
                                            RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                            ExecuteAllScriptsStep executeScriptsStep,
                                            FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(createBackupStep);
            AddStep(resetDBStep);
            AddStep(recreateDBVersionsTablesStep);
            AddStep(executeScriptsStep);
            AddStep(finalizeProcessStep);
        }
    }
}
