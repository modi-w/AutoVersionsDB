using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps;
using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class UpdateProjectConfigProcessDefinition : ProjectConfigProcessDefinition
    {
        public const string Name = "Save New Config Validation";
        public override string EngineTypeName => Name;



        public UpdateProjectConfigProcessDefinition(ValidationsStep<IdExistValidationsFactory> idNotExistValidationStep,
                                                        UpdateProjectConfigStep updateProjectConfigStep,
                                                        CreateMissingFoldersStep createMissingFoldersStep)
        {
            AddStep(idNotExistValidationStep);
            AddStep(updateProjectConfigStep);
            AddStep(createMissingFoldersStep);
        }
    }
}
