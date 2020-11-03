using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.UI.DBVersions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB
{
    public class DevEnv_SyncDB_UI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_SyncDB_API _devEnv_SyncDB_API;
        private readonly DBVersionsViewModel _dbVersionsViewModel;

        public DevEnv_SyncDB_UI(DevEnv_SyncDB_API devEnv_SyncDB_API, DBVersionsViewModel dbVersionsViewModel)
        {
            _devEnv_SyncDB_API = devEnv_SyncDB_API;
            _dbVersionsViewModel = dbVersionsViewModel;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            DBVersionsTestContext testContext = _devEnv_SyncDB_API.Arrange(testArgs) as DBVersionsTestContext;

            MockObjectsProvider.SetTestContextDataByMockCallbacksForUI(testContext);

            _dbVersionsViewModel.SetProjectConfig(testContext.ProjectConfig.Id);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            var task = _dbVersionsViewModel.RunSyncCommand.ExecuteWrapped();
            task.Wait();
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _devEnv_SyncDB_API.Asserts(testContext);

            //AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            //AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            //assertTextByLines.AssertLineMessage("> Run 'sync' for 'IntegrationTestProject'", true);
            //assertTextByLines.AssertLineMessage("The process complete successfully", true);
        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _devEnv_SyncDB_API.Release(testContext);
        }

    }
}
