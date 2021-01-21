using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual
{
    public class DevEnv_Virtual_EmptyDB_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_Virtual_EmptyDB_API _deliveryEnv_Virtual_API;

        public DevEnv_Virtual_EmptyDB_CLI(DevEnv_Virtual_EmptyDB_API deliveryEnv_Virtual_API)
        {
            _deliveryEnv_Virtual_API = deliveryEnv_Virtual_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_deliveryEnv_Virtual_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"virtual -id={IntegrationTestsConsts.TestProjectId} -it={IntegrationTestsConsts.MiddleStateTargetScripts.IncScriptFileName} -rt={IntegrationTestsConsts.MiddleStateTargetScripts.RptScriptFileName} -dt={IntegrationTestsConsts.MiddleStateTargetScripts.DDDScriptFileName}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _deliveryEnv_Virtual_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "virtual").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);
        }



        public override void Release(CLITestContext testContext)
        {
            _deliveryEnv_Virtual_API.Release(testContext);
        }

    }
}
