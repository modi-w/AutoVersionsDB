using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestContexts
{
    public class ProjectConfigTestArgs : TestArgs
    {
        public ProjectConfigItem ProjectConfig { get; }

        public ProjectConfigTestArgs(ProjectConfigItem projectConfig)
        {
            ProjectConfig = projectConfig;
        }
    }
}
