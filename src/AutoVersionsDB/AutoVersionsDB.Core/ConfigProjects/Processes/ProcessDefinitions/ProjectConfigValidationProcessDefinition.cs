using AutoVersionsDB.Core.Common;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class ProjectConfigValidationProcessDefinition : ProjectConfigProcessDefinition
    {
        public override string EngineTypeName => "Project Config Validation";


        public ProjectConfigValidationProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep)
        {
            AddStep(projectConfigValidationStep);
        }
    }
}
