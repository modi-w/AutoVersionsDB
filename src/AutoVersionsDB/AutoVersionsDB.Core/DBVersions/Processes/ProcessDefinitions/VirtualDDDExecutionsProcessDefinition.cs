using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ExecuteScripts;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class VirtualDDDExecutionsProcessDefinition : DBVersionsProcessDefinition
    {
        public const string Name = "Virtual Executions For Dev Dummt Data Files";
        public override string EngineTypeName => Name;



        public VirtualDDDExecutionsProcessDefinition(RestoreDatabaseStep rollbackStep,
                                                ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                                SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                                ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                                ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                                CreateScriptFilesStateStep createScriptFilesStateStep,
                                                ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                                CreateBackupStep createBackupStep,
                                                ExecuteDDDScriptsVirtuallyStep executeDDDScriptsVirtuallyStep,
                                                FinalizeProcessStep finalizeProcessStep)
            : base(rollbackStep, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
            AddStep(createBackupStep);
            AddStep(executeDDDScriptsVirtuallyStep);
            AddStep(finalizeProcessStep);

            IsVirtualExecution = true;
        }
    }
}
