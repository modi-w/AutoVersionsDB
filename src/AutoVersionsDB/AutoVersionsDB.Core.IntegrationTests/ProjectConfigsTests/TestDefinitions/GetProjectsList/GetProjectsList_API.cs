using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
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
        private const string _testProjectCode1 = "TestProject1";
        private const string _testProjectDesc1 = "Test Project 1";

        private const string _testProjectCode2 = "TestProject2";
        private const string _testProjectDesc2 = "Test Project 2";

        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;


        public GetProjectsList_API(ProjectConfigsStorageHelper projectConfigsStorageHelper)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            ProjectConfigItem projectConfig1 = new ProjectConfigItem()
            {
                Id = _testProjectCode1,
                Description = _testProjectDesc1,
            };

            ProjectConfigItem projectConfig2 = new ProjectConfigItem()
            {
                Id = _testProjectCode2,
                Description = _testProjectDesc2,
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig1, projectConfig2);


            return new TestContext(testArgs);
        }


        public override void Act(TestContext testContext)
        {
            testContext.Result = AutoVersionsDBAPI.GetProjectsList();
        }


        public override void Asserts(TestContext testContext)
        {
            List<ProjectConfigItem> projectConfigsResults = testContext.Result as List<ProjectConfigItem>;

            Assert.That(projectConfigsResults.Count == 2, $"{GetType().Name} -> The number projectConfigs DBTypes should be 2, but was: {projectConfigsResults.Count}");

            ProjectConfigItem projectConfig1 = projectConfigsResults[0];
            Assert.That(projectConfig1.Id == _testProjectCode1, $"{GetType().Name} -> ProjectConfig1 Id should be: '{_testProjectCode1}', but was: '{projectConfig1.Id}'");
            Assert.That(projectConfig1.Description == _testProjectDesc1, $"{GetType().Name} -> ProjectConfig1 Description should be: '{_testProjectDesc1}', but was: '{projectConfig1.Description}'");

            ProjectConfigItem projectConfig2 = projectConfigsResults[1];
            Assert.That(projectConfig2.Id == _testProjectCode2, $"{GetType().Name} -> ProjectConfig2 Id should be: '{_testProjectCode2}', but was: '{projectConfig2.Id}'");
            Assert.That(projectConfig2.Description == _testProjectDesc2, $"{GetType().Name} -> ProjectConfig2 Description should be: '{_testProjectDesc2}', but was: '{projectConfig2.Description}'");
        }

        public override void Release(TestContext testContext)
        {
        }
    }
}
