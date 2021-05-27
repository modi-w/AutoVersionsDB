using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class ChangeIdStep : ProjectConfigStep
    {
        public const string Name = "Change Id";
        public override string StepName => Name;

        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public ChangeIdStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ChangeIdProcessArgs projectConfigProcessArgs = processContext.ProcessArgs as ChangeIdProcessArgs;

            _projectConfigsStorage.ChangeId(projectConfigProcessArgs.Id, projectConfigProcessArgs.NewId);
        }

    }
}
