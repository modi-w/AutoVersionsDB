using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps;
using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class ChangeIdProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Save New Config Validation";


        public ChangeIdProcessDefinition(ValidationsStep<IdExistValidationsFactory> idNotExistValidationStep,
                                                        ChangeIdStep changeIdStep)
        {
            AddStep(idNotExistValidationStep);
            AddStep(changeIdStep);
        }
    }
}
