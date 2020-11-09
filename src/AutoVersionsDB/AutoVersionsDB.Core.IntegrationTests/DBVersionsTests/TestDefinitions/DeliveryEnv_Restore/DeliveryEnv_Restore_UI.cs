using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Restore;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Restore
{
    public class DeliveryEnv_Restore_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DeliveryEnv_Restore_API _deliveryEnv_Restore_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DeliveryEnv_Restore_UI(DeliveryEnv_Restore_API deliveryEnv_Restore_API,
                                            DBVersionsViewModel dbVersionsViewModel,
                                            DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _deliveryEnv_Restore_API = deliveryEnv_Restore_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _deliveryEnv_Restore_API.Arrange(testArgs) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            var task1 = _dbVersionsViewModel.RunSyncCommand.ExecuteWrapped();
            task1.Wait();
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_Restore_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertScriptErrorForMiddleState(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertProcessViewStates(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_Restore_API.Release(testContext);
        }
    }
}
