using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files
{
    public class DevEnv_Files_RepeatableChanged_Repeatable_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_Files_RepeatableChanged_API _files_RepeatableChanged_API;

        public DevEnv_Files_RepeatableChanged_Repeatable_CLI(DevEnv_Files_RepeatableChanged_API devEnv_Files_RepeatableChanged_API)
        {
            _files_RepeatableChanged_API = devEnv_Files_RepeatableChanged_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_files_RepeatableChanged_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"files repeatable -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _files_RepeatableChanged_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 8);

            assertTextByLines.AssertLineMessage("> Run 'files repeatable' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);
            assertTextByLines.AssertLineMessage("", true);
            assertTextByLines.AssertLineMessage("++ Repeatable Scripts:", true);
            assertTextByLines.AssertLineMessage("  Status   |  File", true);
            assertTextByLines.AssertLineMessage("-------------------------------------------------------", true);
            assertTextByLines.AssertLineMessage("   changed | rptScript_DataForLookupTable1.sql", true);
            assertTextByLines.AssertLineMessage("   sync    | rptScript_DataForLookupTable2.sql", true);

        }



        public override void Release(CLITestContext testContext)
        {
            _files_RepeatableChanged_API.Release(testContext);
        }

    }
}
