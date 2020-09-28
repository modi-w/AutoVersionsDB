using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public abstract class TestDefinition 
    {

        public abstract TestContext Arrange(ProjectConfigItem projectConfig);

        public abstract void Act(TestContext testContext);


        public abstract void Assert(TestContext testContext);
    }
}
