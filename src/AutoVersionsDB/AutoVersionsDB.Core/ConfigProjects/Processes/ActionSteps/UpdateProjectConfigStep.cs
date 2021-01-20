using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class UpdateProjectConfigStep : ProjectConfigStep
    {
        public const string Name = "Update Project Config";
        public override string StepName => Name;


        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public UpdateProjectConfigStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            _projectConfigsStorage.UpdateProjectConfig(processContext.ProjectConfig);
        }

    }
}
