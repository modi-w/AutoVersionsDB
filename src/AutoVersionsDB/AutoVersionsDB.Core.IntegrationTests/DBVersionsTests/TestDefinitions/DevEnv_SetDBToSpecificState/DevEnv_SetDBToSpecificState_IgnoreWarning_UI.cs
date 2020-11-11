using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SetDBToSpecificState;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.UI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SetDBToSpecificState
{
    public class DevEnv_SetDBToSpecificState_IgnoreWarning_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DevEnv_SetDBToSpecificState_IgnoreWarning_API _devEnv_SetDBToSpecificState_IgnoreWarning_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_SetDBToSpecificState_IgnoreWarning_UI(DevEnv_SetDBToSpecificState_IgnoreWarning_API devEnv_SetDBToSpecificState_IgnoreWarning_API,
                                                        DBVersionsViewModel dbVersionsViewModel,
                                                        DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_SetDBToSpecificState_IgnoreWarning_API = devEnv_SetDBToSpecificState_IgnoreWarning_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }



        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _devEnv_SetDBToSpecificState_IgnoreWarning_API.Arrange(testArgs) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            UIGeneralEvents.OnConfirm += UIGeneralEvents_OnConfirm;

            return testContext;
        }

        public override void Act(DBVersionsAPITestContext testContext)
        {
            _dbVersionsViewModel.DBVersionsViewModelData.TargetStateScriptFileName = IntegrationTestsConsts.TargetStateFile_MiddleState;
            var task1 = _dbVersionsViewModel.ApplySyncSpecificStateCommand.ExecuteWrapped();
            task1.Wait();
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _devEnv_SetDBToSpecificState_IgnoreWarning_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertCompleteSuccessForMiddleState(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertViewStateHistory(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            UIGeneralEvents.OnConfirm -= UIGeneralEvents_OnConfirm;

            _devEnv_SetDBToSpecificState_IgnoreWarning_API.Release(testContext);
        }


        private bool UIGeneralEvents_OnConfirm(object sender, string confirmMessage)
        {
            return true;
        }
    }
}
