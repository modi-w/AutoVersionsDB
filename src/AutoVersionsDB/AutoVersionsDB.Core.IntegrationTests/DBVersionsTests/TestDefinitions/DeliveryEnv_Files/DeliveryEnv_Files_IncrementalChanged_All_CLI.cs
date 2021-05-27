using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Files;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Files
{
    public class DeliveryEnv_Files_IncrementalChanged_All_CLI : TestDefinition<CLITestContext>
    {

        private readonly DeliveryEnv_Files_IncrementalChanged_API _files_IncrementalChanged_API;

        public DeliveryEnv_Files_IncrementalChanged_All_CLI(DeliveryEnv_Files_IncrementalChanged_API devEnv_Files_IncrementalChanged_API)
        {
            _files_IncrementalChanged_API = devEnv_Files_IncrementalChanged_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_files_IncrementalChanged_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"files -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _files_IncrementalChanged_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 17);

            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "files").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);
            assertTextByLines.AssertLineMessage("", true);
            assertTextByLines.AssertLineMessage("++ Incremental Scripts:", true);
            assertTextByLines.AssertLineMessage("  Status   |  File", true);
            assertTextByLines.AssertLineMessage("-------------------------------------------------------", true);
            assertTextByLines.AssertLineMessage("   sync    | incScript_0001_initState.sql", true);
            assertTextByLines.AssertLineMessage("   sync    | incScript_0002_CreateLookupTable1.sql", true);
            assertTextByLines.AssertLineMessage("   changed | incScript_0003_CreateLookupTable2.sql", true);
            assertTextByLines.AssertLineMessage("           | incScript_0004_CreateTransTable1.sql", true);
            assertTextByLines.AssertLineMessage("           | incScript_0005_CreateInvoiceTable1.sql", true);
            assertTextByLines.AssertLineMessage("", true);
            assertTextByLines.AssertLineMessage("++ Repeatable Scripts:", true);
            assertTextByLines.AssertLineMessage("  Status   |  File", true);
            assertTextByLines.AssertLineMessage("-------------------------------------------------------", true);
            assertTextByLines.AssertLineMessage("           | rptScript_0001_DataForLookupTable1.sql", true);
            assertTextByLines.AssertLineMessage("           | rptScript_0002_DataForLookupTable2.sql", true);
        }



        public override void Release(CLITestContext testContext)
        {
            _files_IncrementalChanged_API.Release(testContext);
        }

    }
}
