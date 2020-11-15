using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_New_Incremental_UI : TestDefinition<DBVersionsUITestContext>
    {
        private readonly DeliveryEnv_NotAllowMethods_New_Incremental_API _devEnv_NotAllowMethods_New_Incremental_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DeliveryEnv_NotAllowMethods_New_Incremental_UI(DeliveryEnv_NotAllowMethods_New_Incremental_API devEnv_NotAllowMethods_New_Incremental_API,
                                                                DBVersionsViewModel dbVersionsViewModel,
                                                                DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_NotAllowMethods_New_Incremental_API = devEnv_NotAllowMethods_New_Incremental_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            DBVersionsUITestContext testContext = new DBVersionsUITestContext(_devEnv_NotAllowMethods_New_Incremental_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsUITestContext testContext)
        {
            var task = _dbVersionsViewModel.DeployCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsUITestContext testContext)
        {
            _devEnv_NotAllowMethods_New_Incremental_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertNotAllowMethodDBFinalState(GetType().Name, _dbVersionsViewModel);
            _dbVersionsViewModelAsserts.AssertViewStateHistory(GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }


        public override void Release(DBVersionsUITestContext testContext)
        {
            _devEnv_NotAllowMethods_New_Incremental_API.Release(testContext);
        }

    }
}
