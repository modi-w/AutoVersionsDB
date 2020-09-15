using AutoVersionsDB.Helpers;
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

        private readonly ProjectConfigsStorage _projectConfigsStorage;

        public SaveNewProjectConfigStep(ProjectConfigsStorage projectConfigsStorage)
        {
            projectConfigsStorage.ThrowIfNull(nameof(projectConfigsStorage));

            _projectConfigsStorage = projectConfigsStorage;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            _projectConfigsStorage.SaveNewProjectConfig(processContext.ProjectConfig);
        }

    }
}
