using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class InitDBProcessDefinition : DBVersionsProcessDefinition
    {
        public const string Name = "Initiate DB";
        public override string EngineTypeName => Name;



        public InitDBProcessDefinition(RestoreDatabaseStep rollbackStep,
                                        ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                        ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                        ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                        CreateBackupStep createBackupStep,
                                        RecreateDBVersionsTablesStep recreateDBVersionsTablesStep,
                                        FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(createBackupStep);
            AddStep(recreateDBVersionsTablesStep);
            AddStep(finalizeProcessStep);

            IsVirtualExecution = true;
        }
    }
}
