using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;

using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectConfigById;


using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuGet.Frameworks;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectConfigById
{
    public class GetProjectConfigById_DevEnv_API : TestDefinition
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProjectConfigsStorage _projectConfigsStorage;


        public GetProjectConfigById_DevEnv_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                    ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _projectConfigsStorage = projectConfigsStorage;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.DummyProjectConfig.Id,
                Description = IntegrationTestsConsts.DummyProjectConfig.Description,
                DBType = IntegrationTestsConsts.DummyProjectConfig.DBType,
                Server = IntegrationTestsConsts.DummyProjectConfig.Server,
                DBName = IntegrationTestsConsts.DummyProjectConfig.DBName,
                Username = IntegrationTestsConsts.DummyProjectConfig.Username,
                Password = IntegrationTestsConsts.DummyProjectConfig.Password,
                BackupFolderPath = IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath,
                DevEnvironment = IntegrationTestsConsts.DummyProjectConfig.DevEnvironment,
                DevScriptsBaseFolderPath = IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath,
                DeployArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath,
                DeliveryArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfig.DeliveryArtifactFolderPath,
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig);

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);

            return new TestContext(overrideTestArgs);
        }


        public override void Act(TestContext testContext)
        {
            testContext.Result =
                AutoVersionsDBAPI
                .GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfig.Id);
        }


        public override void Asserts(TestContext testContext)
        {
            ProjectConfigItem projectConfig =
                _projectConfigsStorage.GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfig.Id);

            Assert.That(projectConfig != null, $"{GetType().Name} -> ProjectConfig not found.");

            Assert.That(projectConfig.Description == IntegrationTestsConsts.DummyProjectConfig.Description, $"{GetType().Name} -> ProjectConfig not found.");
        }

        public override void Release(TestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
