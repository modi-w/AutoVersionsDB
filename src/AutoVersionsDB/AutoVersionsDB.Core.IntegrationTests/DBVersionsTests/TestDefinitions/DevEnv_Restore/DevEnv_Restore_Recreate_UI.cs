using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore
{
    public class DevEnv_Restore_Recreate_UI : TestDefinition<DBVersionsUITestContext>
    {
        private readonly DevEnv_Restore_Recreate_API _devEnv_Restore_Recreate_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_Restore_Recreate_UI(DevEnv_Restore_Recreate_API devEnv_Restore_Recreate_API,
                                            DBVersionsViewModel dbVersionsViewModel,
                                            DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_Restore_Recreate_API = devEnv_Restore_Recreate_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;

        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            DBVersionsUITestContext testContext = new DBVersionsUITestContext(_devEnv_Restore_Recreate_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            UIGeneralEvents.OnConfirm += UIGeneralEvents_OnConfirm;

            return testContext;
        }



        public override void Act(DBVersionsUITestContext testContext)
        {
            var task1 = _dbVersionsViewModel.RecreateDBFromScratchCommand.ExecuteWrapped();
            task1.Wait();
        }


        public override void Asserts(DBVersionsUITestContext testContext)
        {
            _devEnv_Restore_Recreate_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertScriptErrorForMiddleState(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertViewStateHistory(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }


        public override void Release(DBVersionsUITestContext testContext)
        {
            UIGeneralEvents.OnConfirm -= UIGeneralEvents_OnConfirm;

            _devEnv_Restore_Recreate_API.Release(testContext);
        }


        private void UIGeneralEvents_OnConfirm(object sender, ConfirmEventArgs e)
        {
            e.IsConfirm = true;
        }

    }
}
