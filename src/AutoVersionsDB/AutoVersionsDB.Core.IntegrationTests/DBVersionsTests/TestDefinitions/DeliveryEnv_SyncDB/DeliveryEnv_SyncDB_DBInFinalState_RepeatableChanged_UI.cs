using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_UI : TestDefinition<DBVersionsTestContext>
    {
        private readonly DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API _deliveryEnv_SyncDB_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_UI(DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API deliveryEnv_SyncDB_API,
                                                                        DBVersionsViewModel dbVersionsViewModel,
                                                                        DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _deliveryEnv_SyncDB_API = deliveryEnv_SyncDB_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsTestContext testContext = _deliveryEnv_SyncDB_API.Arrange(testArgs) as DBVersionsTestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            var task = _dbVersionsViewModel.RunSyncCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Asserts(testContext);
          
            _dbVersionsViewModelAsserts.AssertCompleteSuccessfullyAllFilesSync(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertProcessViewStates(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Release(testContext);
        }

    }
}
