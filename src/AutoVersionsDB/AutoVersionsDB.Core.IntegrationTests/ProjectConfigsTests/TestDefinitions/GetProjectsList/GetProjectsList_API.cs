using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList
{
    public class GetProjectsList_API : TestDefinition
    {
        private const string _testProjectId1 = "TestProject1";
        private const string _testProjectDesc1 = "Test Project 1";

        private const string _testProjectId2 = "TestProject2";
        private const string _testProjectDesc2 = "Test Project 2";

        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;


        public GetProjectsList_API(ProjectConfigsStorageHelper projectConfigsStorageHelper)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            ProjectConfigItem projectConfig1 = new ProjectConfigItem()
            {
                Id = _testProjectId1,
                Description = _testProjectDesc1,
            };

            ProjectConfigItem projectConfig2 = new ProjectConfigItem()
            {
                Id = _testProjectId2,
                Description = _testProjectDesc2,
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig1, projectConfig2);


            return new ProcessTestContext(testArgs);
        }


        public override void Act(ITestContext testContext)
        {
            testContext.Result = AutoVersionsDBAPI.GetProjectsList();
        }


        public override void Asserts(ITestContext testContext)
        {
            List<ProjectConfigItem> projectConfigsResults = testContext.Result as List<ProjectConfigItem>;

            Assert.That(projectConfigsResults.Count == 2, $"{GetType().Name} -> The number projectConfigs DBTypes should be 2, but was: {projectConfigsResults.Count}");

            ProjectConfigItem projectConfig1 = projectConfigsResults[0];
            Assert.That(projectConfig1.Id == _testProjectId1, $"{GetType().Name} -> ProjectConfig1 Id should be: '{_testProjectId1}', but was: '{projectConfig1.Id}'");
            Assert.That(projectConfig1.Description == _testProjectDesc1, $"{GetType().Name} -> ProjectConfig1 Description should be: '{_testProjectDesc1}', but was: '{projectConfig1.Description}'");

            ProjectConfigItem projectConfig2 = projectConfigsResults[1];
            Assert.That(projectConfig2.Id == _testProjectId2, $"{GetType().Name} -> ProjectConfig2 Id should be: '{_testProjectId2}', but was: '{projectConfig2.Id}'");
            Assert.That(projectConfig2.Description == _testProjectDesc2, $"{GetType().Name} -> ProjectConfig2 Description should be: '{_testProjectDesc2}', but was: '{projectConfig2.Description}'");
        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
