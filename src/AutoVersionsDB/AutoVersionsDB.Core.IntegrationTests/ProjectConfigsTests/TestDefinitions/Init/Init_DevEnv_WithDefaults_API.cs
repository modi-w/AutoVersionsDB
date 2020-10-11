using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsUtils;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.DbCommands.Integration;
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


        public Init_DevEnv_WithDefaults_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                    ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _projectConfigsStorage = projectConfigsStorage;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            return new TestContext(testArgs);
        }


        public override void Act(TestContext testContext)
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


        public override void Asserts(TestContext testContext)
        {
            ProjectConfigItem newProjectConfig = _projectConfigsStorage.GetProjectConfigById(IntegrationTestsConsts.DummyProjectConfig.Id);
            Assert.That(newProjectConfig != null, $"{GetType().Name} -> Could not find project with the new ProjectId.");
            Assert.That(newProjectConfig.Description == IntegrationTestsConsts.DummyProjectConfig.Description, $"{GetType().Name} -> Project Description should be: '{ IntegrationTestsConsts.DummyProjectConfig.Description}', but was:'{newProjectConfig.Description}'.");
            Assert.That(newProjectConfig.DBName == IntegrationTestsConsts.DummyProjectConfig.DBName, $"{GetType().Name} -> Project DBName should be: '{ IntegrationTestsConsts.DummyProjectConfig.DBName}', but was:'{newProjectConfig.DBName}'.");
            Assert.That(newProjectConfig.Username == IntegrationTestsConsts.DummyProjectConfig.Username, $"{GetType().Name} -> Project Username should be: '{ IntegrationTestsConsts.DummyProjectConfig.Username}', but was:'{newProjectConfig.Username}'.");
            Assert.That(newProjectConfig.Password == IntegrationTestsConsts.DummyProjectConfig.Password, $"{GetType().Name} -> Project Password should be: '{ IntegrationTestsConsts.DummyProjectConfig.Password}', but was:'{newProjectConfig.Password}'.");
            Assert.That(newProjectConfig.DevEnvironment == true, $"{GetType().Name} -> Project DevEnvironment should be: '{ IntegrationTestsConsts.DummyProjectConfig.DevEnvironment}', but was:'{newProjectConfig.DevEnvironment}'.");
            Assert.That(newProjectConfig.DevScriptsBaseFolderPath == IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath, $"{GetType().Name} -> Project DevScriptsBaseFolderPath should be: '{ IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath}', but was:'{newProjectConfig.DevScriptsBaseFolderPath}'.");
            Assert.That(newProjectConfig.DeployArtifactFolderPath == IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath, $"{GetType().Name} -> Project DeployArtifactFolderPath should be: '{ IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath}', but was:'{newProjectConfig.DeployArtifactFolderPath}'.");
        
            Assert.That(newProjectConfig.DBType == "SqlServer", $"{GetType().Name} -> Project DBType should be: 'SqlServer' (as default value), but was:'{newProjectConfig.DBType}'.");
            Assert.That(newProjectConfig.Server == "(local)", $"{GetType().Name} -> Project Server should be: '(local)' (as default value), but was:'{newProjectConfig.Server}'.");
            Assert.That(newProjectConfig.BackupFolderPath == @"C:\ProgramData\AutoVersionsDB\Backups\IntegrationTestProject", $@"{GetType().Name} -> Project BackupFolderPath should be: 'C:\ProgramData\AutoVersionsDB\Backups\IntegrationTestProject' (as default value), but was:'{newProjectConfig.BackupFolderPath}'.");
        }

        public override void Release(TestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
