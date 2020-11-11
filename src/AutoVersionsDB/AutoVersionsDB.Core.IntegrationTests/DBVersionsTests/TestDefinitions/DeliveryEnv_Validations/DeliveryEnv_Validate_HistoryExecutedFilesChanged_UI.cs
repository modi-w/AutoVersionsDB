using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.UIAsserts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations
{
    public class DeliveryEnv_Validate_HistoryExecutedFilesChanged_UI : TestDefinition<DBVersionsAPITestContext>
    {
        private readonly DeliveryEnv_Validate_HistoryExecutedFilesChanged_API _devEnv_Validate_HistoryExecutedFilesChanged_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;
        private readonly DBVersionsViewModelAsserts _dbVersionsViewModelAsserts;

        public DeliveryEnv_Validate_HistoryExecutedFilesChanged_UI(DeliveryEnv_Validate_HistoryExecutedFilesChanged_API devEnv_Validate_HistoryExecutedFilesChanged_API,
                                                                DBVersionsViewModel dbVersionsViewModel,
                                                                DBVersionsViewModelAsserts dbVersionsViewModelAsserts)
        {
            _devEnv_Validate_HistoryExecutedFilesChanged_API = devEnv_Validate_HistoryExecutedFilesChanged_API;
            _dbVersionsViewModel = dbVersionsViewModel;
            _dbVersionsViewModelAsserts = dbVersionsViewModelAsserts;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsAPITestContext testContext = _devEnv_Validate_HistoryExecutedFilesChanged_API.Arrange(testArgs) as DBVersionsAPITestContext;

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
            _devEnv_Validate_HistoryExecutedFilesChanged_API.Asserts(testContext);

            _dbVersionsViewModelAsserts.AssertIncrementalChanged(this.GetType().Name, _dbVersionsViewModel, testContext.ProjectConfig.DevEnvironment);
            _dbVersionsViewModelAsserts.AssertViewStateHistory(this.GetType().Name, testContext.ViewStatesHistory, DBVersionsViewStateType.HistoryExecutedFilesChanged);
        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _devEnv_Validate_HistoryExecutedFilesChanged_API.Release(testContext);
        }

    }
}
