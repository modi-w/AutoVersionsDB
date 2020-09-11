using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.Processes.DBVersionsProcesses;
using AutoVersionsDB.Core.ScriptFiles;
using AutoVersionsDB.Core.ScriptFiles.Incremental;
using AutoVersionsDB.Core.ScriptFiles.Repeatable;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace AutoVersionsDB.Core.ProcessSteps.ProjectConfig
{
    public class UpdateProjectConfigStep : ProjectConfigStep
    {
        public override string StepName => "Update New Project Config";

        private readonly ProjectConfigs _projectConfigs;

        internal UpdateProjectConfigStep(ProjectConfigs projectConfigs)
        {
            projectConfigs.ThrowIfNull(nameof(projectConfigs));

            _projectConfigs = projectConfigs;

        }



        public override void Execute(ProjectConfigProcessContext processContext)
        {
            _projectConfigs.UpdateProjectConfig(processContext.ProjectConfig);
        }

    }
}
