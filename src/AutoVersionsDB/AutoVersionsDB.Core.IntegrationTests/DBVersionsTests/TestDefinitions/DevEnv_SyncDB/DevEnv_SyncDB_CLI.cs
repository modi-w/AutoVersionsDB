using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB
{
    public class DevEnv_SyncDB_CLI : TestDefinition<DBVersionsAPITestContext>
    {

        private readonly DevEnv_SyncDB_API _devEnv_SyncDB_API;

        public DevEnv_SyncDB_CLI(DevEnv_SyncDB_API devEnv_SyncDB_API)
        {
            _devEnv_SyncDB_API = devEnv_SyncDB_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _devEnv_SyncDB_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"sync -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _devEnv_SyncDB_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage("> Run 'sync' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _devEnv_SyncDB_API.Release(testContext);
        }

    }
}
