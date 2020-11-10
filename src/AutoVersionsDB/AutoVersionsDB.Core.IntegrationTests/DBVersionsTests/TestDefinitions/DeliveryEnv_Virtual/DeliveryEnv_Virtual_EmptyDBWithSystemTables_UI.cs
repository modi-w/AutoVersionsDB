using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual
{
    public class DeliveryEnv_Virtual_EmptyDBWithSystemTables_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DeliveryEnv_Virtual_EmptyDBWithSystemTables_API _deliveryEnv_Virtual_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DeliveryEnv_Virtual_EmptyDBWithSystemTables_UI(DeliveryEnv_Virtual_EmptyDBWithSystemTables_API deliveryEnv_Virtual_API,
                                                            DBVersionsViewModel dbVersionsViewModel,
                                                            DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _deliveryEnv_Virtual_API = deliveryEnv_Virtual_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _deliveryEnv_Virtual_API.Arrange(testArgs) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            _dbVersionsViewModel.DBVersionsViewModelData.TargetStateScriptFileName = IntegrationTestsConsts.TargetStateFile_MiddleState;
            var task = _dbVersionsViewModel.RunSetDBStateManallyCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_Virtual_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertCompleteSuccessForMiddleState(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertProcessViewStates(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_Virtual_API.Release(testContext);
        }

    }
}
