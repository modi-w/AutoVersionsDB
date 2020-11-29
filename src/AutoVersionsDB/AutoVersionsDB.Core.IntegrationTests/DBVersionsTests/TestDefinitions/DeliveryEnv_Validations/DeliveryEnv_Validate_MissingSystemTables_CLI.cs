using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations
{
    public class DeliveryEnv_Validate_MissingSystemTables_CLI : TestDefinition<CLITestContext>
    {

        private readonly DeliveryEnv_Validate_MissingSystemTables_API _deliveryEnv_Validate_MissingSystemTables_API;

        public DeliveryEnv_Validate_MissingSystemTables_CLI(DeliveryEnv_Validate_MissingSystemTables_API deliveryEnv_Validate_MissingSystemTables_API)
        {
            _deliveryEnv_Validate_MissingSystemTables_API = deliveryEnv_Validate_MissingSystemTables_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_deliveryEnv_Validate_MissingSystemTables_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _deliveryEnv_Validate_MissingSystemTables_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 1);
            assertConsoleOutTextByLines.AssertLineMessage("> Run 'validate' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError, 5);
            assertErrorsTextByLines.AssertLineMessage("The process complete with errors:", true);
            assertErrorsTextByLines.AssertLineMessage("--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage("SystemTables. Error: The table 'AutoVersionsDB.DBScriptsExecutionHistory' is not exist in the db", false);
            assertErrorsTextByLines.AssertLineMessage("", true);
            assertErrorsTextByLines.AssertLineMessage("The system tables has invalid structure. Please try to 'Set DB State by Virtual Execution'.", true);
        }



        public override void Release(CLITestContext testContext)
        {
            _deliveryEnv_Validate_MissingSystemTables_API.Release(testContext);
        }


    }
}
