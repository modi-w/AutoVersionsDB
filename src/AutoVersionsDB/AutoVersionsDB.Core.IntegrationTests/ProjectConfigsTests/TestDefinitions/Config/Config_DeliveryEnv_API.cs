using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config
{
    public class Config_DeliveryEnv_API : TestDefinition
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly ProcessAsserts _processAsserts;
        private readonly ProjectConfigsDirectories _projectConfigsDirectoriesCleaner;
        private readonly PropertiesAsserts _properiesAsserts;



        public Config_DeliveryEnv_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                ProjectConfigsStorage projectConfigsStorage,
                                ProcessAsserts processAsserts,
                                ProjectConfigsDirectories projectConfigsDirectoriesCleaner,
                                PropertiesAsserts properiesAsserts)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _projectConfigsStorage = projectConfigsStorage;
            _processAsserts = processAsserts;
            _projectConfigsDirectoriesCleaner = projectConfigsDirectoriesCleaner;
            _properiesAsserts = properiesAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            _projectConfigsDirectoriesCleaner.ClearAutoCreatedFolders();

            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.DummyProjectConfig.Id,
                DevEnvironment = false
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig);

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);

            return new ProcessTestContext(overrideTestArgs);
        }


        public override void Act(ITestContext testContext)
        {
            testContext.ProjectConfig.Description = IntegrationTestsConsts.DummyProjectConfig.Description;
            testContext.ProjectConfig.DBType = IntegrationTestsConsts.DummyProjectConfig.DBType;
            testContext.ProjectConfig.Server = IntegrationTestsConsts.DummyProjectConfig.Server;
            testContext.ProjectConfig.DBName = IntegrationTestsConsts.DummyProjectConfig.DBName;
            testContext.ProjectConfig.Username = IntegrationTestsConsts.DummyProjectConfig.Username;
            testContext.ProjectConfig.Password = IntegrationTestsConsts.DummyProjectConfig.Password;
            testContext.ProjectConfig.BackupFolderPath = IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath;
            testContext.ProjectConfig.DeliveryArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfig.DeliveryArtifactFolderPath;

            testContext.ProcessResults = AutoVersionsDBAPI.UpdateProjectConfig(testContext.ProjectConfig, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            ProjectConfigItem newProjectConfig = _projectConfigsStorage.GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfig.Id);
            Assert.That(newProjectConfig != null, $"{GetType().Name} -> Could not find project with the new ProjectId.");

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Description), newProjectConfig.Description, IntegrationTestsConsts.DummyProjectConfig.Description);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBName),  newProjectConfig.DBName, IntegrationTestsConsts.DummyProjectConfig.DBName);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Username),  newProjectConfig.Username, IntegrationTestsConsts.DummyProjectConfig.Username);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Password),  newProjectConfig.Password, IntegrationTestsConsts.DummyProjectConfig.Password);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DevEnvironment),  newProjectConfig.DevEnvironment, false);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DeliveryArtifactFolderPath),  newProjectConfig.DeliveryArtifactFolderPath, IntegrationTestsConsts.DummyProjectConfig.DeliveryArtifactFolderPath);

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBType),  newProjectConfig.DBType, IntegrationTestsConsts.DummyProjectConfig.DBType);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Server),  newProjectConfig.Server, IntegrationTestsConsts.DummyProjectConfig.Server);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.BackupFolderPath),  newProjectConfig.BackupFolderPath, IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath);

            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath);
            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfig.DeliveryArtifactFolderPath);

        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigsDirectoriesCleaner.ClearAutoCreatedFolders();

            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
