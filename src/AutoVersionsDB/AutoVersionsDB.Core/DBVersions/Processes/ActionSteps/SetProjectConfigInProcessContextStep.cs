using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.DBVersions.Processes.ActionSteps
{
    public class SetProjectConfigInProcessContextStep : DBVersionsStep
    {
        public const string Name = "Set Project Config In Process Context";
        public override string StepName => Name;


        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public SetProjectConfigInProcessContextStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;
        }



        public override void Execute(DBVersionsProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            string id = (processContext.ProcessArgs as DBVersionsProcessArgs).Id;

            processContext.SetProjectConfig(_projectConfigsStorage.GetProjectConfigById(id));

        }
    }
}