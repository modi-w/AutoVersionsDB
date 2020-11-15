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
using AutoVersionsDB.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init
{
    public class Init_DevEnv_WithDefaults_API : TestDefinition
    {
        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly ProcessAsserts _processAsserts;
        private readonly ProjectConfigsDirectories _projectConfigsDirectories;
        private readonly PropertiesAsserts _properiesAsserts;


        public Init_DevEnv_WithDefaults_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
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
                Id = IntegrationTestsConsts.DummyProjectConfig.Id,
                Description = IntegrationTestsConsts.DummyProjectConfig.Description,
                DBName = IntegrationTestsConsts.DummyProjectConfig.DBName,
                Username = IntegrationTestsConsts.DummyProjectConfig.Username,
                Password = IntegrationTestsConsts.DummyProjectConfig.Password,
                DevEnvironment = true,
                DevScriptsBaseFolderPath = IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath,
                DeployArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath,
            };

            testContext.ProcessResults = AutoVersionsDBAPI.SaveNewProjectConfig(projectConfig, null);
        }


        public override void Asserts(ITestContext testContext)
        {
            _processAsserts.AssertProccessValid(GetType().Name, testContext.ProcessResults.Trace);


            ProjectConfigItem newProjectConfig = _projectConfigsStorage.GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfig.Id);
            Assert.That(newProjectConfig != null, $"{GetType().Name} -> Could not find project with the new ProjectId.");

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Description), newProjectConfig.Description, IntegrationTestsConsts.DummyProjectConfig.Description);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBName), newProjectConfig.DBName, IntegrationTestsConsts.DummyProjectConfig.DBName);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Username), newProjectConfig.Username, IntegrationTestsConsts.DummyProjectConfig.Username);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Password), newProjectConfig.Password, IntegrationTestsConsts.DummyProjectConfig.Password);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DevEnvironment), newProjectConfig.DevEnvironment, true);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DevScriptsBaseFolderPath), newProjectConfig.DevScriptsBaseFolderPath, IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath);
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DeployArtifactFolderPath), newProjectConfig.DeployArtifactFolderPath, IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath);

            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.DBType), newProjectConfig.DBType, "SqlServer");
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.Server), newProjectConfig.Server, "(local)");
            _properiesAsserts.AssertProperty(GetType().Name, nameof(newProjectConfig.BackupFolderPath), newProjectConfig.BackupFolderPath, ProjectConfigsDirectories.DefaultBackupsFolder);

            _projectConfigsDirectories.AssertDirectoryExist(GetType().Name, newProjectConfig.BackupFolderPath);
            _projectConfigsDirectories.AssertDirectoryExist(GetType().Name, newProjectConfig.DevScriptsBaseFolderPath);
            _projectConfigsDirectories.AssertDirectoryExist(GetType().Name, newProjectConfig.IncrementalScriptsFolderPath);
            _projectConfigsDirectories.AssertDirectoryExist(GetType().Name, newProjectConfig.RepeatableScriptsFolderPath);
            _projectConfigsDirectories.AssertDirectoryExist(GetType().Name, newProjectConfig.DevDummyDataScriptsFolderPath);
            _projectConfigsDirectories.AssertDirectoryExist(GetType().Name, newProjectConfig.DeployArtifactFolderPath);
        }

        public override void Release(ITestContext testContext)
        {
            _projectConfigsDirectories.ClearAutoCreatedFolders();

            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
