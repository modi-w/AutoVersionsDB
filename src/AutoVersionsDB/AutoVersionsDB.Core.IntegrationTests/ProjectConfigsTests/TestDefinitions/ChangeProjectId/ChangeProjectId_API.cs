﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
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

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId
{
    public class ChangeProjectId_API : TestDefinition
    {
        public const string OldProjectId = "TestProject_Old";
        public const string NewProjectId = "TestProject_New";
        public const string ProjectDesc = "Test Project Desc";


        private readonly ProjectConfigsStorageHelper _projectConfigsStorageHelper;
        private readonly ProjectConfigsStorage _projectConfigsStorage;


        public ChangeProjectId_API(ProjectConfigsStorageHelper projectConfigsStorageHelper,
                                    ProjectConfigsStorage projectConfigsStorage)
        {
            _projectConfigsStorageHelper = projectConfigsStorageHelper;
            _projectConfigsStorage = projectConfigsStorage;
        }


        public override TestContext Arrange(TestArgs testArgs)
        {
            ProjectConfigItem projectConfig1 = new ProjectConfigItem()
            {
                Id = OldProjectId,
                Description = ProjectDesc,
            };

            _projectConfigsStorageHelper.PrepareTestProject(projectConfig1);


            return new TestContext(testArgs);
        }


        public override void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDBAPI.ChangeProjectId(OldProjectId, NewProjectId, null);
        }


        public override void Asserts(TestContext testContext)
        {
            ProjectConfigItem oldProjectByProjectId = _projectConfigsStorage.GetProjectConfigById(OldProjectId);
            Assert.That(oldProjectByProjectId == null, $"{this.GetType().Name} -> Shuold not find project with the old ProjectId.");

            ProjectConfigItem newProjectByProjectId = _projectConfigsStorage.GetProjectConfigById(NewProjectId);
            Assert.That(newProjectByProjectId != null, $"{this.GetType().Name} -> Could not find project with the new ProjectId.");
            Assert.That(newProjectByProjectId.Id == NewProjectId, $"{this.GetType().Name} -> The new ProjectId should be: '{NewProjectId}', but was: '{newProjectByProjectId.Id}'");
            Assert.That(newProjectByProjectId.Description == ProjectDesc, $"{this.GetType().Name} -> Project Description should be: '{ProjectDesc}', but was:'{newProjectByProjectId.Description}'.");
        }

        public override void Release(TestContext testContext)
        {
            _projectConfigsStorageHelper.ClearAllProjects();
        }
    }
}