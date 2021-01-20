using AutoVersionsDB.Core.Common;
using AutoVersionsDB.NotificationableEngine.Validations;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions
{
    public class ProjectConfigValidationProcessDefinition : ProjectConfigProcessDefinition
    {
        public const string Name = "Project Config Validation";
        public override string EngineTypeName => Name;



        public ProjectConfigValidationProcessDefinition(ValidationsStep<ProjectConfigValidationsFactory> projectConfigValidationStep)
        {
            AddStep(projectConfigValidationStep);
        }
    }
}
