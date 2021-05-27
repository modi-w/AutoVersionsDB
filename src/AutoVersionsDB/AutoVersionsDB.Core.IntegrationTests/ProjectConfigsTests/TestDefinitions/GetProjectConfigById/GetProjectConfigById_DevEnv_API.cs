using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectConfigById;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using NuGet.Frameworks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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


        public override ITestContext Arrange(TestArgs testArgs)
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.DummyProjectConfigValid.Id,
                Description = IntegrationTestsConsts.DummyProjectConfigValid.Description,
                DBType = IntegrationTestsConsts.DummyProjectConfigValid.DBType,
                Server = IntegrationTestsConsts.DummyProjectConfigValid.Server,
                DBName = IntegrationTestsConsts.DummyProjectConfigValid.DBName,
                Username = IntegrationTestsConsts.DummyProjectConfigValid.Username,
                Password = IntegrationTestsConsts.DummyProjectConfigValid.Password,
                BackupFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath,
                DevEnvironment = IntegrationTestsConsts.DummyProjectConfigValid.DevEnvironment,
                DevScriptsBaseFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.DevScriptsBaseFolderPath,
                DeployArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.DeployArtifactFolderPath,
                DeliveryArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.DeliveryArtifactFolderPath,
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig);

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);

            return new ProcessTestContext(overrideTestArgs);
        }


        public override void Act(ITestContext testContext)
        {
            testContext.Result =
                AutoVersionsDBAPI
                .GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfigValid.Id);
        }


        public override void Asserts(ITestContext testContext)
        {
            ProjectConfigItem projectConfig =
                _projectConfigsStorage.GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfigValid.Id);

            Assert.That(projectConfig != null, $"{GetType().Name} >>> ProjectConfig not found.");

            Assert.That(projectConfig.Description == IntegrationTestsConsts.DummyProjectConfigValid.Description, $"{GetType().Name} >>> ProjectConfig not found.");
        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
