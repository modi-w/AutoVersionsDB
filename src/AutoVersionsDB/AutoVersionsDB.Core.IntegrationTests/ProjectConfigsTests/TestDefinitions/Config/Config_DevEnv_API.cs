using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
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

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config
{
    public class Config_DevEnv_API : TestDefinition<TestContext<ProjectConfigTestArgs>>
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly ProcessAsserts _processAsserts;
        private readonly ProjectConfigsDirectories _projectConfigsDirectoriesCleaner;
        private readonly ProperiesAsserts _properiesAsserts;



        public Config_DevEnv_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                ProjectConfigsStorage projectConfigsStorage,
                                ProcessAsserts processAsserts,
                                ProjectConfigsDirectories projectConfigsDirectoriesCleaner,
                                ProperiesAsserts properiesAsserts)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _projectConfigsStorage = projectConfigsStorage;
            _processAsserts = processAsserts;
            _projectConfigsDirectoriesCleaner = projectConfigsDirectoriesCleaner;
            _properiesAsserts = properiesAsserts;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            _projectConfigsDirectoriesCleaner.ClearAutoCreatedFolders();

            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.DummyProjectConfig.Id,
                DevEnvironment = true
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig);

            ProjectConfigTestArgs overrideTestArgs = new ProjectConfigTestArgs(projectConfig);

            return new TestContext<ProjectConfigTestArgs>(overrideTestArgs);
        }


        public override void Act(TestContext<ProjectConfigTestArgs> testContext)
        {
            testContext.TestArgs.ProjectConfig.Description = IntegrationTestsConsts.DummyProjectConfig.Description;
            testContext.TestArgs.ProjectConfig.DBType = IntegrationTestsConsts.DummyProjectConfig.DBType;
            testContext.TestArgs.ProjectConfig.Server = IntegrationTestsConsts.DummyProjectConfig.Server;
            testContext.TestArgs.ProjectConfig.DBName = IntegrationTestsConsts.DummyProjectConfig.DBName;
            testContext.TestArgs.ProjectConfig.Username = IntegrationTestsConsts.DummyProjectConfig.Username;
            testContext.TestArgs.ProjectConfig.Password = IntegrationTestsConsts.DummyProjectConfig.Password;
            testContext.TestArgs.ProjectConfig.BackupFolderPath = IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath;
            testContext.TestArgs.ProjectConfig.DevScriptsBaseFolderPath = IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath;
            testContext.TestArgs.ProjectConfig.DeployArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath;

            testContext.ProcessResults = AutoVersionsDBAPI.UpdateProjectConfig(testContext.TestArgs.ProjectConfig, null);
        }


        public override void Asserts(TestContext<ProjectConfigTestArgs> testContext)
        {
            ProjectConfigItem newProjectConfig = _projectConfigsStorage.GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfig.Id);
            Assert.That(newProjectConfig != null, $"{GetType().Name} -> Could not find project with the new ProjectId.");

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Description), IntegrationTestsConsts.DummyProjectConfig.Description, newProjectConfig.Description);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBName), IntegrationTestsConsts.DummyProjectConfig.DBName, newProjectConfig.DBName);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Username), IntegrationTestsConsts.DummyProjectConfig.Username, newProjectConfig.Username);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Password), IntegrationTestsConsts.DummyProjectConfig.Password, newProjectConfig.Password);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DevEnvironment), true, newProjectConfig.DevEnvironment);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DevScriptsBaseFolderPath), IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath, newProjectConfig.DevScriptsBaseFolderPath);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DeployArtifactFolderPath), IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath, newProjectConfig.DeployArtifactFolderPath);

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBType), IntegrationTestsConsts.DummyProjectConfig.DBType, newProjectConfig.DBType);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Server), IntegrationTestsConsts.DummyProjectConfig.Server, newProjectConfig.Server);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.BackupFolderPath), IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath, newProjectConfig.BackupFolderPath);

            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath);
            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath);
            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfig.IncrementalScriptsFolderPath);
            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfig.RepeatableScriptsFolderPath);
            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfig.DevDummyDataScriptsFolderPath);
            _projectConfigsDirectoriesCleaner.AssertDirectoryExist(GetType().Name, IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath);
        }

        public override void Release(TestContext<ProjectConfigTestArgs> testContext)
        {
            _projectConfigsDirectoriesCleaner.ClearAutoCreatedFolders();

            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
