using AutoVersionsDB.CLI;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_VirtualDDD
{
    public class DevEnv_VirtualDDD_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_VirtualDDD_API _devEnv_VirtualDDD_API;

        public DevEnv_VirtualDDD_CLI(DevEnv_VirtualDDD_API devEnv_VirtualDDD_API)
        {
            _devEnv_VirtualDDD_API = devEnv_VirtualDDD_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_VirtualDDD_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"vrddd -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_VirtualDDD_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "vrddd").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);
        }



        public override void Release(CLITestContext testContext)
        {
            _devEnv_VirtualDDD_API.Release(testContext);
        }

    }
}
