using AutoVersionsDB.Common;
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

        private readonly ProjectConfigs _projectConfigs;

        public ChangeProjectCodeStep(ProjectConfigs projectConfigs)
        {
            projectConfigs.ThrowIfNull(nameof(projectConfigs));

            _projectConfigs = projectConfigs;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            ChangeProjectCodeProcessParams projectConfigProcessParams = processContext.ProcessParams as ChangeProjectCodeProcessParams;

            _projectConfigs.ChangeProjectCode(projectConfigProcessParams.ProjectCode, projectConfigProcessParams.NewProjectCode);
        }

    }
}
