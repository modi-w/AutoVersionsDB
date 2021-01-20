using AutoVersionsDB.Core.Common;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.DBVersions.Processes.ProcessDefinitions
{
    public class DeployProcessDefinition : DBVersionsProcessDefinition
    {
        public const string Name = "Deploy";
        public override string EngineTypeName => Name;


        public DeployProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep,
                                        ValidationsStep<IdExistDBVersionsValidationsFactory> idExistValidationStep,
                                        SetProjectConfigInProcessContextStep setProjectConfigInProcessContextStep,
                                        ValidationsStep<CheckDeliveryEnvValidationsFactory> checkDeliveryEnvValidationStep,
                                        CreateScriptFilesStateStep createScriptFilesStateStep,
                                        ValidationsStep<SystemTableValidationsFactory> systemTableValidationStep,
                                        ValidationsStep<DBStateValidationsFactory> dbStateValidationStep,
                                        BuildDeployArtifactFileStep buildDeployArtifactFileStep)
            : base(null, idExistValidationStep, setProjectConfigInProcessContextStep)
        {
            AddStep(projectConfigValidationStep);
            AddStep(checkDeliveryEnvValidationStep);
            AddStep(createScriptFilesStateStep);
            AddStep(systemTableValidationStep);
            AddStep(dbStateValidationStep);
            AddStep(buildDeployArtifactFileStep);
        }
    }
}
