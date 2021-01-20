using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.Common.Validators;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Config;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Init;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.Validations
{
    public class ProjectConfigValidation_DevEnv_NotValid_UI : TestDefinition<EditProjectUITestContext>
    {
        private readonly ProjectConfigValidation_DevEnv_NotValid_API _projectConfigValidation_DevEnv_NotValid_API;
        private readonly EditProjectViewModel _editProjectViewModel;
        private readonly EditProjectViewModelAsserts _editProjectViewModelAsserts;
        private readonly EditProjectViewStateAsserts _editProjectViewStateAsserts;


        public ProjectConfigValidation_DevEnv_NotValid_UI(ProjectConfigValidation_DevEnv_NotValid_API projectConfigValidation_DevEnv_NotValid_API,
                                    EditProjectViewModel editProjectViewModel,
                                    EditProjectViewModelAsserts editProjectViewModelAsserts,
                                    EditProjectViewStateAsserts editProjectViewStateAsserts)
        {
            _projectConfigValidation_DevEnv_NotValid_API = projectConfigValidation_DevEnv_NotValid_API;
            _editProjectViewModel = editProjectViewModel;
            _editProjectViewModelAsserts = editProjectViewModelAsserts;
            _editProjectViewStateAsserts = editProjectViewStateAsserts;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            EditProjectUITestContext testContext = new EditProjectUITestContext(_projectConfigValidation_DevEnv_NotValid_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _editProjectViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(EditProjectUITestContext testContext)
        {
            var task = _editProjectViewModel.SaveCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(EditProjectUITestContext testContext)
        {
            _projectConfigValidation_DevEnv_NotValid_API.Asserts(testContext);

            _editProjectViewStateAsserts.AssertEditProjectViewStateUpdate(GetType().Name, _editProjectViewModel.EditProjectControls, true);
            _editProjectViewStateAsserts.AssertError(
                GetType().Name,
                _editProjectViewModel.NotificationsViewModel.NotificationsViewModelData,
                _editProjectViewModel.EditProjectControls,
                _editProjectViewModel.ProjectConfigErrorMessages,
                new List<string>()
                {
                    DBTypeValidator.Name,
                    DBNameValidator.Name,
                    DBBackupFolderValidator.Name,
                    DevScriptsBaseFolderPathValidator.Name,
                    DeployArtifactFolderPathValidator.Name,
                }
                );
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
            _projectConfigValidation_DevEnv_NotValid_API.Release(testContext);

        }

    }
}
