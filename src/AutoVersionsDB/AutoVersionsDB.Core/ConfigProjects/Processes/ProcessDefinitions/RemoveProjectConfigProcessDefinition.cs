using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps;
using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class RemoveProjectConfigProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Save New Config Validation";


        public RemoveProjectConfigProcessDefinition(ValidationsStep<IdExistValidationsFactory> idNotExistValidationStep,
                                                        RemoveProjectConfigStep removeProjectConfigStep)
        {
            AddStep(idNotExistValidationStep);
            AddStep(removeProjectConfigStep);
        }
    }
}
