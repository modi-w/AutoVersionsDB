using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init
{
    public class Init_DevEnv_AllProperties_UI : TestDefinition<EditProjectUITestContext>
    {
        private readonly Init_DevEnv_AllProperties_API _init_DevEnv_AllProperties_API;
        private readonly EditProjectViewModel _editProjectViewModel;
        private readonly EditProjectViewModelAsserts _editProjectViewModelAsserts;
        private readonly EditProjectViewStateAsserts _editProjectViewStateAsserts;


        public Init_DevEnv_AllProperties_UI(Init_DevEnv_AllProperties_API init_DevEnv_AllProperties_API,
                                    EditProjectViewModel editProjectViewModel,
                                    EditProjectViewModelAsserts editProjectViewModelAsserts,
                                    EditProjectViewStateAsserts editProjectViewStateAsserts)
        {
            _init_DevEnv_AllProperties_API = init_DevEnv_AllProperties_API;
            _editProjectViewModel = editProjectViewModel;
            _editProjectViewModelAsserts = editProjectViewModelAsserts;
            _editProjectViewStateAsserts = editProjectViewStateAsserts;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            EditProjectUITestContext testContext = new EditProjectUITestContext(_init_DevEnv_AllProperties_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _editProjectViewModel.CreateNewProjectConfig();
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(EditProjectUITestContext testContext)
        {
            _editProjectViewModel.ProjectConfig.Id = IntegrationTestsConsts.DummyProjectConfigValid.Id;
            _editProjectViewModel.ProjectConfig.Description = IntegrationTestsConsts.DummyProjectConfigValid.Description;
            _editProjectViewModel.ProjectConfig.DBType = IntegrationTestsConsts.DummyProjectConfigValid.DBType;
            _editProjectViewModel.ProjectConfig.Server = IntegrationTestsConsts.DummyProjectConfigValid.Server;
            _editProjectViewModel.ProjectConfig.DBName = IntegrationTestsConsts.DummyProjectConfigValid.DBName;
            _editProjectViewModel.ProjectConfig.Username = IntegrationTestsConsts.DummyProjectConfigValid.Username;
            _editProjectViewModel.ProjectConfig.Password = IntegrationTestsConsts.DummyProjectConfigValid.Password;
            _editProjectViewModel.ProjectConfig.BackupFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath;
            _editProjectViewModel.ProjectConfig.DevEnvironment = true;
            _editProjectViewModel.ProjectConfig.DevScriptsBaseFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.DevScriptsBaseFolderPath;
            _editProjectViewModel.ProjectConfig.DeployArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.DeployArtifactFolderPath;

            var task = _editProjectViewModel.SaveCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(EditProjectUITestContext testContext)
        {
            _init_DevEnv_AllProperties_API.Asserts(testContext);

            _editProjectViewStateAsserts.AssertEditProjectViewStateUpdate(GetType().Name, _editProjectViewModel.EditProjectControls, true);
            _editProjectViewStateAsserts.AssertNoErrors(GetType().Name, _editProjectViewModel.EditProjectControls, _editProjectViewModel.ProjectConfigErrorMessages);
            _editProjectViewModelAsserts.AssertViewStateHistory(GetType().Name,
                testContext.ViewStatesHistory,
                new List<EditProjectViewStateType>()
                    {
                        EditProjectViewStateType.InProcess,
                        EditProjectViewStateType.InProcess,
                        EditProjectViewStateType.Update
                    }
                );

        }


        public override void Release(EditProjectUITestContext testContext)
        {
            _init_DevEnv_AllProperties_API.Release(testContext);

        }

    }
}
