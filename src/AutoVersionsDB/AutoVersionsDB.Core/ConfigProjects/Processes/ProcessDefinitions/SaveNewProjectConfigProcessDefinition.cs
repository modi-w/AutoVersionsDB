using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps;
using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class SaveNewProjectConfigProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Save New Config Validation";


        public SaveNewProjectConfigProcessDefinition(ValidationsStep<IdNotExistValidationsFactory> idNotExistValidationStep,
                                                        SaveNewProjectConfigStep saveNewProjectConfigStep,
                                                        CreateMissingFoldersStep createMissingFoldersStep)
        {
            AddStep(idNotExistValidationStep);
            AddStep(saveNewProjectConfigStep);
            AddStep(createMissingFoldersStep);
        }
    }
}
