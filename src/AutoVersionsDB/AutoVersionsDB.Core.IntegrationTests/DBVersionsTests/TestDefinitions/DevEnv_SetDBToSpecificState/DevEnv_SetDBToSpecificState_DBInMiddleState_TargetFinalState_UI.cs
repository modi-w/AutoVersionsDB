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
    public class DevEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_UI : TestDefinition<DBVersionsTestContext>
    {
        private readonly DevEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API _devEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_UI(DevEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API devEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API,
                                                        DBVersionsViewModel dbVersionsViewModel,
                                                        DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API = devEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }



        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsTestContext testContext = _devEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API.Arrange(testArgs) as DBVersionsTestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            UIGeneralEvents.OnConfirm += UIGeneralEvents_OnConfirm;

            return testContext;
        }

        public override void Act(DBVersionsTestContext testContext)
        {
            _dbVersionsViewModel.DBVersionsViewModelData.TargetStateScriptFileName = IntegrationTestsConsts.TargetStateFile_FinalState;
            var task1 = _dbVersionsViewModel.ApplySyncSpecificStateCommand.ExecuteWrapped();
            task1.Wait();
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _devEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertCompleteSuccessfullyAllFilesSync(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertProcessViewStates(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }


        public override void Release(DBVersionsTestContext testContext)
        {
            UIGeneralEvents.OnConfirm -= UIGeneralEvents_OnConfirm;

            _devEnv_SetDBToSpecificState_DBInMiddleState_TargetFinalState_API.Release(testContext);
        }


        private bool UIGeneralEvents_OnConfirm(object sender, string confirmMessage)
        {
            return true;
        }
    }
}
