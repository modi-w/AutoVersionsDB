using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public class TestContext
    {
        public ProjectConfigItem ProjectConfig { get; }
        public NumOfConnections NumOfConnectionsBefore { get; set; }
        public ProcessResults ProcessResults { get; set; }

        public TestContext(ProjectConfigItem projectConfig)
        {
            ProjectConfig = projectConfig;
        }

    }
}
