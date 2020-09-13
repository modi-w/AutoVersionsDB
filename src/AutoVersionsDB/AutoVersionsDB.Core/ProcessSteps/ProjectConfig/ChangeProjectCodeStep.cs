﻿using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ArtifactFile;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.ProcessDefinitions;
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
            ProjectConfigProcessParams projectConfigProcessParams = processContext.ProcessParams as ProjectConfigProcessParams;

            _projectConfigs.ChangeProjectCode(projectConfigProcessParams.ProjectCode, projectConfigProcessParams.NewProjectCode);
        }

    }
}
