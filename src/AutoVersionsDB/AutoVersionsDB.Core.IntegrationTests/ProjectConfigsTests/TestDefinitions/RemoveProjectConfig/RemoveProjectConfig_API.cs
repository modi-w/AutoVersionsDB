using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.RemoveProjectConfig;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.RemoveProjectConfig
{
    public class RemoveProjectConfig_API : TestDefinition
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly ProcessAsserts _processAsserts;


        public RemoveProjectConfig_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                    ProjectConfigsStorage projectConfigsStorage,
                                    ProcessAsserts processAsserts)

        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _projectConfigsStorage = projectConfigsStorage;
            _processAsserts = processAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.TestProjectId,
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig);

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);


            return new TestContext(overrideTestArgs);
        }


        public override void Act(TestContext testContext)
        {
            testContext.ProcessResults = 
                AutoVersionsDBAPI
                .RemoveProjectConfig(
                    (testContext.TestArgs as ProjectConfigTestArgs).ProjectConfig.Id, null);
        }


        public override void Asserts(TestContext testContext)
        {
            _processAsserts.AssertProccessValid(GetType().Name, testContext.ProcessResults.Trace);

            ProjectConfigItem projectByProjectId = 
                _projectConfigsStorage.GetProjectConfigById(
                    (testContext.TestArgs as ProjectConfigTestArgs).ProjectConfig.Id);
            Assert.That(projectByProjectId == null, $"{GetType().Name} -> ProjectConfig didnt remove from storage.");
        }

        public override void Release(TestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
