using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init;
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

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init
{
    public class Init_DeliveryEnv_AllProperties_API : TestDefinition
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly ProcessAsserts _processAsserts;
        private readonly ProjectConfigsDirectories _projectConfigsDirectories;
        private readonly PropertiesAsserts _properiesAsserts;


        public Init_DeliveryEnv_AllProperties_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                                ProjectConfigsStorage projectConfigsStorage,
                                                ProcessAsserts processAsserts,
                                                ProjectConfigsDirectories projectConfigsDirectories,
                                                PropertiesAsserts properiesAsserts)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _projectConfigsStorage = projectConfigsStorage;
            _processAsserts = processAsserts;
            _projectConfigsDirectories = projectConfigsDirectories;
            _properiesAsserts = properiesAsserts;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            _projectConfigsDirectories.ClearAutoCreatedFolders();

            return new ProcessTestContext(testArgs);
        }


        public override void Act(ITestContext testContext)
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
                DevEnvironment = false,
                DeliveryArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.DeliveryArtifactFolderPath,
            };

            testContext.ProcessResults = AutoVersionsDBAPI.SaveNewProjectConfig(projectConfig, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _processAsserts.AssertProccessValid(GetType().Name, testContext.ProcessResults.Trace);

            ProjectConfigItem newProjectConfig = _projectConfigsStorage.GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfigValid.Id);
            Assert.That(newProjectConfig != null, $"{GetType().Name} -> Could not find project with the new ProjectId.");

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Description), newProjectConfig.Description, IntegrationTestsConsts.DummyProjectConfigValid.Description);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBName), newProjectConfig.DBName, IntegrationTestsConsts.DummyProjectConfigValid.DBName);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Username), newProjectConfig.Username, IntegrationTestsConsts.DummyProjectConfigValid.Username);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Password), newProjectConfig.Password, IntegrationTestsConsts.DummyProjectConfigValid.Password);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DevEnvironment), newProjectConfig.DevEnvironment, false);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DeliveryArtifactFolderPath), newProjectConfig.DeliveryArtifactFolderPath, IntegrationTestsConsts.DummyProjectConfigValid.DeliveryArtifactFolderPath);

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBType), newProjectConfig.DBType, IntegrationTestsConsts.DummyProjectConfigValid.DBType);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Server), newProjectConfig.Server, IntegrationTestsConsts.DummyProjectConfigValid.Server);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.BackupFolderPath), newProjectConfig.BackupFolderPath, IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath);

            _projectConfigsDirectories.AssertDirectoryExist(GetType().Name, newProjectConfig.BackupFolderPath);
            _projectConfigsDirectories.AssertDirectoryExist(GetType().Name, newProjectConfig.DeliveryArtifactFolderPath);
        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigsDirectories.ClearAutoCreatedFolders();

            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
