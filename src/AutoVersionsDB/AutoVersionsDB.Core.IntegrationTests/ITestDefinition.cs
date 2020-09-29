using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public interface ITestDefinition
    {
        TestContext Arrange(ProjectConfigItem projectConfig);
        void Act(TestContext testContext);
        void Asserts(TestContext testContext);
    }
}