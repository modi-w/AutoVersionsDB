using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId;
using AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.ChooseProject;
using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.ChangeProjectId
{
    public class ChangeProjectId_UI : TestDefinition<EditProjectAPITestContext>
    {
        private readonly ChangeProjectId_API _changeProjectId_API;
        private readonly EditProjectViewModel _editProjectViewModel;
        private readonly EditProjectViewModelAsserts _editProjectViewModelAsserts;
        private readonly EditProjectViewStateAsserts _editProjectViewStateAsserts;
        

        public ChangeProjectId_UI(ChangeProjectId_API changeProjectId_API,
                                    EditProjectViewModel editProjectViewModel,
                                    EditProjectViewModelAsserts editProjectViewModelAsserts,
                                    EditProjectViewStateAsserts editProjectViewStateAsserts)
        {
            _changeProjectId_API = changeProjectId_API;
            _editProjectViewModel = editProjectViewModel;
            _editProjectViewModelAsserts = editProjectViewModelAsserts;
            _editProjectViewStateAsserts = editProjectViewStateAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            EditProjectAPITestContext testContext = _changeProjectId_API.Arrange(testArgs) as EditProjectAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _editProjectViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(EditProjectAPITestContext testContext)
        {
            var task = _editProjectViewModel.SetEditIdStateCommand.ExecuteWrapped();
            task.Wait();


            _editProjectViewModel.ProjectConfig.Id = ChangeProjectId_API.NewProjectId;
            var task2 = _editProjectViewModel.SaveChangeIdCommand.ExecuteWrapped();
            task2.Wait();
        }


        public override void Asserts(EditProjectAPITestContext testContext)
        {
            _changeProjectId_API.Asserts(testContext);

            _editProjectViewStateAsserts.AssertEditProjectViewStateUpdate(this.GetType().Name, _editProjectViewModel.EditProjectControls, testContext.ProjectConfig.DevEnvironment);
            _editProjectViewStateAsserts.AssertNoErrors(this.GetType().Name, _editProjectViewModel.EditProjectControls, _editProjectViewModel.ProjectConfigErrorMessages);
            _editProjectViewModelAsserts.AssertViewStateHistory(this.GetType().Name, testContext.ViewStatesHistory, EditProjectViewStateType.Update);

        }


        public override void Release(EditProjectAPITestContext testContext)
        {
            _changeProjectId_API.Release(testContext);

        }

    }
}
