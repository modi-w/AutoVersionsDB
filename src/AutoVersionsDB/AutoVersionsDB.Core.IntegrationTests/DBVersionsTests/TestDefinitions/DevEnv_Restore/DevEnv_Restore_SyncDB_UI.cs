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
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore
{
    public class DevEnv_Restore_SyncDB_UI : TestDefinition<DBVersionsUITestContext>
    {
        private readonly DevEnv_Restore_SyncDB_API _devEnv_Restore_SyncDB_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DevEnv_Restore_SyncDB_UI(DevEnv_Restore_SyncDB_API devEnv_Restore_SyncDB_API,
                                            DBVersionsViewModel dbVersionsViewModel,
                                            DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_Restore_SyncDB_API = devEnv_Restore_SyncDB_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            DBVersionsUITestContext testContext = new DBVersionsUITestContext(_devEnv_Restore_SyncDB_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsUITestContext testContext)
        {
            var task1 = _dbVersionsViewModel.RunSyncCommand.ExecuteWrapped();
            task1.Wait();
        }


        public override void Asserts(DBVersionsUITestContext testContext)
        {
            _devEnv_Restore_SyncDB_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertScriptErrorForMiddleState(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertViewStateHistory(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsUITestContext testContext)
        {
            _devEnv_Restore_SyncDB_API.Release(testContext);
        }
    }
}
