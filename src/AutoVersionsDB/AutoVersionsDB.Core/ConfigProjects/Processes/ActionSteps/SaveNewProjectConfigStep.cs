using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class SaveNewProjectConfigStep : ProjectConfigStep
    {
        public override string StepName => "Save New Project Config";

        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public SaveNewProjectConfigStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            processContext.ProjectConfig.SetDefaltValues();
            _projectConfigsStorage.SaveNewProjectConfig(processContext.ProjectConfig);
        }

    }
}
