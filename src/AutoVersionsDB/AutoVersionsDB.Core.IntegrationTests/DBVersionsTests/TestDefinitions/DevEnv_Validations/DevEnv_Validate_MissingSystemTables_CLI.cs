using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations
{
    public class DevEnv_Validate_MissingSystemTables_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_Validate_MissingSystemTables_API _devEnv_Validate_MissingSystemTables_API;

        public DevEnv_Validate_MissingSystemTables_CLI(DevEnv_Validate_MissingSystemTables_API devEnv_Validate_MissingSystemTables_API)
        {
            _devEnv_Validate_MissingSystemTables_API = devEnv_Validate_MissingSystemTables_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _devEnv_Validate_MissingSystemTables_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _devEnv_Validate_MissingSystemTables_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertConsoleOutTextByLines.AssertLineMessage(0, "> Run 'validate' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertErrorsTextByLines.AssertLineMessage(0, "The process complete with errors:", true);
            assertErrorsTextByLines.AssertLineMessage(1, "--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage(2, "SystemTables. Error: The table 'AutoVersionsDB.DBScriptsExecutionHistory' is not exist in the db", false);
            assertErrorsTextByLines.AssertLineMessage(3, "", true);
            assertErrorsTextByLines.AssertLineMessage(4, "The system tables has invalid structure. Please try to 'Recreate DB From Scratch' or 'Set DB State by Virtual Execution'.", true);


        }


        public override void Release(DBVersionsTestContext testContext)
        {
            _devEnv_Validate_MissingSystemTables_API.Release(testContext);
        }

    }
}
