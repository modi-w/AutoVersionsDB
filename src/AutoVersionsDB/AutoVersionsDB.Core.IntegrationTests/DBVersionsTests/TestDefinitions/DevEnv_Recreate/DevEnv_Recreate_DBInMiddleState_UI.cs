﻿using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.DBVersions;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate
{
    public class DevEnv_Recreate_DBInMiddleState_UI : TestDefinition<DBVersionsUITestContext>
    {
        private readonly DevEnv_Recreate_DBInMiddleState_API _devEnv_Recreate_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_Recreate_DBInMiddleState_UI(DevEnv_Recreate_DBInMiddleState_API devEnv_Recreate_API,
                                                    DBVersionsViewModel dbVersionsViewModel,
                                                    DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_Recreate_API = devEnv_Recreate_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            DBVersionsUITestContext testContext = new DBVersionsUITestContext(_devEnv_Recreate_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            UIGeneralEvents.OnConfirm += UIGeneralEvents_OnConfirm;


            return testContext;
        }


        public override void Act(DBVersionsUITestContext testContext)
        {
            var task = _dbVersionsViewModel.RecreateDBFromScratchCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsUITestContext testContext)
        {
            _devEnv_Recreate_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertCompleteSuccessfullyAllFilesSync(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertViewStateHistory(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsUITestContext testContext)
        {
            UIGeneralEvents.OnConfirm -= UIGeneralEvents_OnConfirm;

            _devEnv_Recreate_API.Release(testContext);
        }



        private void UIGeneralEvents_OnConfirm(object sender, ConfirmEventArgs e)
        {
            e.IsConfirm = true;
        }

    }
}
