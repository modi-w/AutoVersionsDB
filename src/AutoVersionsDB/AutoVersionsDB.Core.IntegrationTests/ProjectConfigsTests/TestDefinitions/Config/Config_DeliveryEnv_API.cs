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
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ProjectConfigsUtils;
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
        private readonly DBHandler _dbHandler;



        public Config_DeliveryEnv_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                ProjectConfigsStorage projectConfigsStorage,
                                ProcessAsserts processAsserts,
                                ProjectConfigsDirectories projectConfigsDirectoriesCleaner,
                                PropertiesAsserts properiesAsserts,
                                DBHandler dbHandler)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _projectConfigsStorage = projectConfigsStorage;
            _processAsserts = processAsserts;
            _projectConfigsDirectoriesCleaner = projectConfigsDirectoriesCleaner;
            _properiesAsserts = properiesAsserts;
            _dbHandler = dbHandler;
        }


        public override ITestContext Arrange(TestArgs testArgs)
        {
            _projectConfigsDirectoriesCleaner.ClearAutoCreatedFolders();

            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.DummyProjectConfigValid.Id,
                DevEnvironment = false
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig);

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);

            _dbHandler.RestoreDB(IntegrationTestsConsts.DummyProjectConfigValid.DBConnectionInfo, DBBackupFileType.EmptyDB);

            return new ProcessTestContext(overrideTestArgs);
        }


        public override void Act(ITestContext testContext)
        {
            testContext.ProjectConfig.Description = IntegrationTestsConsts.DummyProjectConfigValid.Description;
            testContext.ProjectConfig.DBType = IntegrationTestsConsts.DummyProjectConfigValid.DBType;
            testContext.ProjectConfig.Server = IntegrationTestsConsts.DummyProjectConfigValid.Server;
            testContext.ProjectConfig.DBName = IntegrationTestsConsts.DummyProjectConfigValid.DBName;
            testContext.ProjectConfig.Username = IntegrationTestsConsts.DummyProjectConfigValid.Username;
            testContext.ProjectConfig.Password = IntegrationTestsConsts.DummyProjectConfigValid.Password;
            testContext.ProjectConfig.BackupFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath;
            testContext.ProjectConfig.DeliveryArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.DeliveryArtifactFolderPath;

            testContext.ProcessResults = AutoVersionsDBAPI.UpdateProjectConfig(testContext.ProjectConfig, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            ProjectConfigItem newProjectConfig = _projectConfigsStorage.GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfigValid.Id);
            Assert.That(newProjectConfig != null, $"{GetType().Name} >>> Could not find project with the new ProjectId.");

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Description), newProjectConfig.Description, IntegrationTestsConsts.DummyProjectConfigValid.Description);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBName), newProjectConfig.DBName, IntegrationTestsConsts.DummyProjectConfigValid.DBName);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Username), newProjectConfig.Username, IntegrationTestsConsts.DummyProjectConfigValid.Username);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Password), newProjectConfig.Password, IntegrationTestsConsts.DummyProjectConfigValid.Password);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DevEnvironment), newProjectConfig.DevEnvironment, false);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DeliveryArtifactFolderPath), newProjectConfig.DeliveryArtifactFolderPath, IntegrationTestsConsts.DummyProjectConfigValid.DeliveryArtifactFolderPath);

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBType), newProjectConfig.DBType, IntegrationTestsConsts.DummyProjectConfigValid.DBType);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Server), newProjectConfig.Server, IntegrationTestsConsts.DummyProjectConfigValid.Server);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.BackupFolderPath), newProjectConfig.BackupFolderPath, IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath);

            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath);
            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfigValid.DeliveryArtifactFolderPath);

        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigsDirectoriesCleaner.ClearAutoCreatedFolders();
           
            _dbHandler.DropDB(IntegrationTestsConsts.DummyProjectConfigValid.DBConnectionInfo);

            _projectConfigsStorageHelper.ClearAllProjects();

        }
    }
}
