using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy
{
    public class DevEnv_Deploy_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_Deploy_API _devEnv_Deploy_API;

        public DevEnv_Deploy_CLI(DevEnv_Deploy_API devEnv_Deploy_API)
        {
            _devEnv_Deploy_API = devEnv_Deploy_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_Deploy_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"deploy -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_Deploy_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,3);
            assertTextByLines.AssertLineMessage("> Run 'deploy' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);
            assertTextByLines.AssertLineMessage("Artifact file created: ", false,2);
            assertTextByLines.AssertLineMessage(@"Deploy\AutoVersionsDB.Tests.avdb'", false,2);
        }



        public override void Release(CLITestContext testContext)
        {
            _devEnv_Deploy_API.Release(testContext);
        }
    }
}
