using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class SaveNewProjectConfigStep : ProjectConfigStep
    {
        public const string Name = "Save New Project Config";
        public override string StepName => Name;


        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public SaveNewProjectConfigStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            processContext.ProjectConfig.SetDefaltValues();
            _projectConfigsStorage.SaveNewProjectConfig(processContext.ProjectConfig);
        }

    }
}
