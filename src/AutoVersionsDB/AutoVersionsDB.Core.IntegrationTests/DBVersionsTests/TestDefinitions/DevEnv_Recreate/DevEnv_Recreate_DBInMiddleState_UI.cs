using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Recreate
{
    public class DevEnv_Recreate_DBInMiddleState_UI : TestDefinition<DBVersionsAPITestContext>
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

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _devEnv_Recreate_API.Arrange(testArgs) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            UIGeneralEvents.OnConfirm += UIGeneralEvents_OnConfirm;


            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            var task = _dbVersionsViewModel.RecreateDbFromScratchCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _devEnv_Recreate_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertCompleteSuccessfullyAllFilesSync(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertProcessViewStates(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            UIGeneralEvents.OnConfirm -= UIGeneralEvents_OnConfirm;

            _devEnv_Recreate_API.Release(testContext);
        }



        private bool UIGeneralEvents_OnConfirm(object sender, string confirmMessage)
        {
            return true;
        }

    }
}
