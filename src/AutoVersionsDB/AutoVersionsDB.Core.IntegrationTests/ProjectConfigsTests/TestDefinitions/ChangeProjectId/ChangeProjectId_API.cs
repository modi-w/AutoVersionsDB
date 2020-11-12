using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetDBTypes;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.GetProjectsList;
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

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId
{
    public class ChangeProjectId_API : TestDefinition<EditProjectAPITestContext>
    {
        public const string OldProjectId = "TestProject_Old";
        public const string NewProjectId = "TestProject_New";
        public const string ProjectDesc = "Test Project Desc";


        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProjectConfigsStorage _projectConfigsStorage;
        private readonly ProcessAsserts _processAsserts;


        public ChangeProjectId_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
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
                Id = OldProjectId,
                Description = ProjectDesc,
                BackupFolderPath = IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath,
                DBName = IntegrationTestsConsts.DummyProjectConfig.DBName,
                DBType = IntegrationTestsConsts.DummyProjectConfig.DBType,
                DeliveryArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfig.BackupFolderPath,
                DeployArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfig.DeployArtifactFolderPath,
                DevScriptsBaseFolderPath= IntegrationTestsConsts.DummyProjectConfig.DevScriptsBaseFolderPath,
                DevEnvironment = true,
                Password = IntegrationTestsConsts.DummyProjectConfig.Password,
                Server = IntegrationTestsConsts.DummyProjectConfig.Server,
                Username = IntegrationTestsConsts.DummyProjectConfig.Username,
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig);

            ProjectConfigTestArgs projectConfigTestArgs = new ProjectConfigTestArgs(projectConfig);

            return new EditProjectAPITestContext(projectConfigTestArgs);
        }


        public override void Act(EditProjectAPITestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ChangeProjectId(OldProjectId, NewProjectId, null);
        }


        public override void Asserts(EditProjectAPITestContext testContext)
        {
            _processAsserts.AssertProccessValid(GetType().Name, testContext.ProcessResults.Trace);

            ProjectConfigItem oldProjectByProjectId = _projectConfigsStorage.GetProjectConfigById(OldProjectId);
            Assert.That(oldProjectByProjectId == null, $"{this.GetType().Name} -> Shuold not find project with the old ProjectId.");

            ProjectConfigItem newProjectByProjectId = _projectConfigsStorage.GetProjectConfigById(NewProjectId);
            Assert.That(newProjectByProjectId != null, $"{this.GetType().Name} -> Could not find project with the new ProjectId.");
            Assert.That(newProjectByProjectId.Id == NewProjectId, $"{this.GetType().Name} -> The new ProjectId should be: '{NewProjectId}', but was: '{newProjectByProjectId.Id}'");
            Assert.That(newProjectByProjectId.Description == ProjectDesc, $"{this.GetType().Name} -> Project Description should be: '{ProjectDesc}', but was:'{newProjectByProjectId.Description}'.");
        }

        public override void Release(EditProjectAPITestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}
