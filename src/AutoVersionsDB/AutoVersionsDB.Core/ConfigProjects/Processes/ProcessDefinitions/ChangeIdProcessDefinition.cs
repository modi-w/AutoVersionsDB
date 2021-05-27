using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps;
using AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps.ValidationFactories;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class ChangeIdProcessDefinition : ProjectConfigProcessDefinition
    {
        public const string Name = "Save New Config Validation";
        public override string EngineTypeName => Name;


        public ChangeIdProcessDefinition(ValidationsStep<IdExistValidationsFactory> idNotExistValidationStep,
                                                        ChangeIdStep changeIdStep)
        {
            AddStep(idNotExistValidationStep);
            AddStep(changeIdStep);
        }
    }
}
