using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Files;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.DB;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.Process;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Files
{
    public class DeliveryEnv_Files_RepeatableChanged_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DBVersionsTestHelper _dbVersionsTestHelper;
        private readonly ProcessAsserts _processAsserts;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DeliveryEnv_Files_RepeatableChanged_UI(DBVersionsTestHelper dbVersionsTestHelper,
                                                        ProcessAsserts processAsserts,
                                                        DBVersionsViewModel dbVersionsViewModel,
                                                        DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _dbVersionsTestHelper = dbVersionsTestHelper;
            _processAsserts = processAsserts;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _dbVersionsTestHelper.Arrange(testArgs, false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.RepeatableChanged) as DBVersionsAPITestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);
            testContext.ClearProcessData();

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            var task = _dbVersionsViewModel.RefreshAllCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Asserts(testContext, true);

            _dbVersionsViewModelAsserts.AssertRepeatableChanged(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertProcessViewStates(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.ReadyToRunSync);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _dbVersionsTestHelper.Release(testContext);
        }

    }
}
