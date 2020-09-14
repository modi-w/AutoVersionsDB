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
    public class UpdateProjectConfigStep : ProjectConfigStep
    {
        public override string StepName => "Update Project Config";

        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public UpdateProjectConfigStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            _projectConfigsStorage.UpdateProjectConfig(processContext.ProjectConfig);
        }

    }
}
