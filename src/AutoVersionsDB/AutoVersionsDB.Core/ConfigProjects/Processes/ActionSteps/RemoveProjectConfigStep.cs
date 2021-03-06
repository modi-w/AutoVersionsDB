﻿using AutoVersionsDB.Helpers;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class RemoveProjectConfigStep : ProjectConfigStep
    {
        public const string Name = "Remove Project Config";
        public override string StepName => Name;


        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public RemoveProjectConfigStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            processContext.ThrowIfNull(nameof(processContext));

            ProjectConfigProcessArgs projectConfigProcessArgs = processContext.ProcessArgs as ProjectConfigProcessArgs;

            _projectConfigsStorage.RemoveProjectConfig(projectConfigProcessArgs.Id);
        }

    }
}
