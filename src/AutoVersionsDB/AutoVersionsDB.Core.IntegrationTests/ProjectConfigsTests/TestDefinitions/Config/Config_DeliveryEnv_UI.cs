using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config
{
    public class Config_DeliveryEnv_UI : TestDefinition<EditProjectUITestContext>
    {
        private readonly Config_DeliveryEnv_API _Config_DeliveryEnv_API;
        private readonly EditProjectViewModel _editProjectViewModel;
        private readonly EditProjectViewModelAsserts _editProjectViewModelAsserts;
        private readonly EditProjectViewStateAsserts _editProjectViewStateAsserts;


        public Config_DeliveryEnv_UI(Config_DeliveryEnv_API Config_DeliveryEnv_API,
                                    EditProjectViewModel editProjectViewModel,
                                    EditProjectViewModelAsserts editProjectViewModelAsserts,
                                    EditProjectViewStateAsserts editProjectViewStateAsserts)
        {
            _Config_DeliveryEnv_API = Config_DeliveryEnv_API;
            _editProjectViewModel = editProjectViewModel;
            _editProjectViewModelAsserts = editProjectViewModelAsserts;
            _editProjectViewStateAsserts = editProjectViewStateAsserts;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            EditProjectUITestContext testContext = new EditProjectUITestContext(_Config_DeliveryEnv_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _editProjectViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(EditProjectUITestContext testContext)
        {
            _editProjectViewModel.ProjectConfig.Description = IntegrationTestsConsts.DummyProjectConfigValid.Description;
            _editProjectViewModel.ProjectConfig.Description = IntegrationTestsConsts.DummyProjectConfigValid.Description;
            _editProjectViewModel.ProjectConfig.DBType = IntegrationTestsConsts.DummyProjectConfigValid.DBType;
            _editProjectViewModel.ProjectConfig.Server = IntegrationTestsConsts.DummyProjectConfigValid.Server;
            _editProjectViewModel.ProjectConfig.DBName = IntegrationTestsConsts.DummyProjectConfigValid.DBName;
            _editProjectViewModel.ProjectConfig.Username = IntegrationTestsConsts.DummyProjectConfigValid.Username;
            _editProjectViewModel.ProjectConfig.Password = IntegrationTestsConsts.DummyProjectConfigValid.Password;
            _editProjectViewModel.ProjectConfig.BackupFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.BackupFolderPath;
            _editProjectViewModel.ProjectConfig.DeliveryArtifactFolderPath = IntegrationTestsConsts.DummyProjectConfigValid.DeliveryArtifactFolderPath;

            var task = _editProjectViewModel.SaveCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(EditProjectUITestContext testContext)
        {
            _Config_DeliveryEnv_API.Asserts(testContext);

            _editProjectViewStateAsserts.AssertEditProjectViewStateUpdate(GetType().Name, _editProjectViewModel.EditProjectControls, testContext.ProjectConfig.DevEnvironment);
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
            _Config_DeliveryEnv_API.Release(testContext);

        }

    }
}
