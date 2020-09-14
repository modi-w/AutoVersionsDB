using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace AutoVersionsDB.Core.ConfigProjects.Processes.ActionSteps
{
    public class SaveNewProjectConfigStep : ProjectConfigStep
    {
        public override string StepName => "Save New Project Config";

        private readonly ProjectConfigs _projectConfigs;

        public SaveNewProjectConfigStep(ProjectConfigs projectConfigs)
        {
            projectConfigs.ThrowIfNull(nameof(projectConfigs));

            _projectConfigs = projectConfigs;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            _projectConfigs.SaveNewProjectConfig(processContext.ProjectConfig);
        }

    }
}
