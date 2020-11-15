using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB
{
    public class DevEnv_SyncDB_RepeatableChanged_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_SyncDB_RepeatableChanged_API _devEnv_SyncDB_RepeatableChanged_API;

        public DevEnv_SyncDB_RepeatableChanged_CLI(DevEnv_SyncDB_RepeatableChanged_API devEnv_SyncDB_RepeatableChanged_API)
        {
            _devEnv_SyncDB_RepeatableChanged_API = devEnv_SyncDB_RepeatableChanged_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_SyncDB_RepeatableChanged_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"sync -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_SyncDB_RepeatableChanged_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,2);
            assertTextByLines.AssertLineMessage("> Run 'sync' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);
        }


        public override void Release(CLITestContext testContext)
        {
            _devEnv_SyncDB_RepeatableChanged_API.Release(testContext);
        }

    }
}
