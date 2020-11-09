using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_API _deliveryEnv_SyncDB_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_UI(DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_API deliveryEnv_SyncDB_API,
                                                                            DBVersionsViewModel dbVersionsViewModel,
                                                                            DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _deliveryEnv_SyncDB_API = deliveryEnv_SyncDB_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _deliveryEnv_SyncDB_API.Arrange(testArgs) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            var task = _dbVersionsViewModel.RunSyncCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertCompleteSuccessfullyAllFilesSync(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertProcessViewStates(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Release(testContext);
        }

    }
}
