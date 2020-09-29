using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests
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
