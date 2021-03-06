﻿using AutoVersionsDB.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB
{
    public class DevEnv_SyncDB_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_SyncDB_API _devEnv_SyncDB_API;

        public DevEnv_SyncDB_CLI(DevEnv_SyncDB_API devEnv_SyncDB_API)
        {
            _devEnv_SyncDB_API = devEnv_SyncDB_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_SyncDB_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"sync -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_SyncDB_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "sync").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);
        }



        public override void Release(CLITestContext testContext)
        {
            _devEnv_SyncDB_API.Release(testContext);
        }

    }
}
