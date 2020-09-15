using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ConfigProjects.Processes.ProcessDefinitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class ChangeProjectCodeStep : ProjectConfigStep
    {
        public override string StepName => "Change Project Code";

        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public ChangeProjectCodeStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            ChangeProjectCodeProcessParams projectConfigProcessParams = processContext.ProcessParams as ChangeProjectCodeProcessParams;

            _projectConfigsStorage.ChangeProjectCode(projectConfigProcessParams.ProjectCode, projectConfigProcessParams.NewProjectCode);
        }

    }
}
