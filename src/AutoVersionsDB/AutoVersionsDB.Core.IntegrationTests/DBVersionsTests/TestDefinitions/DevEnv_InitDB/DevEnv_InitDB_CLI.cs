using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_InitDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_InitDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_InitDB;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_InitDB
{
    public class DevEnv_InitDB_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_InitDB_API _devEnv_InitDB_API;

        public DevEnv_InitDB_CLI(DevEnv_InitDB_API devEnv_InitDB_API)
        {
            _devEnv_InitDB_API = devEnv_InitDB_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_InitDB_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"initdb -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_InitDB_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "initdb").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);
        }



        public override void Release(CLITestContext testContext)
        {
            _devEnv_InitDB_API.Release(testContext);
        }

    }
}
