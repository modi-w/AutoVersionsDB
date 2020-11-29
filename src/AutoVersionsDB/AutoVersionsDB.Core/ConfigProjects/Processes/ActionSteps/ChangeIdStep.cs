using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class ChangeIdStep : ProjectConfigStep
    {
        public override string StepName => "Change Id";

        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public ChangeIdStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            ChangeIdProcessParams projectConfigProcessParams = processContext.ProcessParams as ChangeIdProcessParams;

            _projectConfigsStorage.ChangeId(projectConfigProcessParams.Id, projectConfigProcessParams.NewId);
        }

    }
}
