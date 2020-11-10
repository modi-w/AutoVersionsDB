using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_SetDBToSpecificState_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DeliveryEnv_NotAllowMethods_SetDBToSpecificState_API _devEnv_NotAllowMethods_SetDBToSpecificState_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DeliveryEnv_NotAllowMethods_SetDBToSpecificState_UI(DeliveryEnv_NotAllowMethods_SetDBToSpecificState_API devEnv_NotAllowMethods_SetDBToSpecificState_API,
                                                                DBVersionsViewModel dbVersionsViewModel,
                                                                DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_NotAllowMethods_SetDBToSpecificState_API = devEnv_NotAllowMethods_SetDBToSpecificState_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _devEnv_NotAllowMethods_SetDBToSpecificState_API.Arrange(testArgs) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            var task = _dbVersionsViewModel.DeployCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _devEnv_NotAllowMethods_SetDBToSpecificState_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertNotAllowMethodDBFinalState(GetType().Name, _dbVersionsViewModel);
            _dbVersionsViewModelAsserts.AssertProcessViewStates(GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _devEnv_NotAllowMethods_SetDBToSpecificState_API.Release(testContext);
        }

    }
}
