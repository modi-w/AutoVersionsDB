using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class RemoveProjectConfigStep : ProjectConfigStep
    {
        public override string StepName => "Remove Project Config";

        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public RemoveProjectConfigStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ProjectConfigProcessParams projectConfigProcessParams = processContext.ProcessParams as ProjectConfigProcessParams;

            _projectConfigsStorage.RemoveProjectConfig(projectConfigProcessParams.Id);
        }

    }
}
