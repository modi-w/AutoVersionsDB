using AutoVersionsDB.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore
{
    public class DevEnv_Restore_SyncDB_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_Restore_SyncDB_API _devEnv_Restore_SyncDB_API;

        public DevEnv_Restore_SyncDB_CLI(DevEnv_Restore_SyncDB_API devEnv_Restore_SyncDB_API)
        {
            _devEnv_Restore_SyncDB_API = devEnv_Restore_SyncDB_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_Restore_SyncDB_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"sync -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_Restore_SyncDB_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 1);
            assertConsoleOutTextByLines.AssertLineMessage("> Run 'sync' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError, null);
            assertErrorsTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteWithErrors, true);
            assertErrorsTextByLines.AssertLineMessage("--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage("incScript_0004_CreateTransTable1.sql 0% (0/1) >>> Execute Script Block. Error: System.Exception: Error Message: 'Column, parameter, or variable #3: Cannot find data type nvarcharaaaa.', Script: ", false);
        }



        public override void Release(CLITestContext testContext)
        {
            _devEnv_Restore_SyncDB_API.Release(testContext);
        }
    }
}
