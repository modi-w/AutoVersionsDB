using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations
{
    public class DeliveryEnv_Validate_MissingSystemTables_CLI : ITestDefinition
    {

        private readonly DeliveryEnv_Validate_MissingSystemTables_API _deliveryEnv_Validate_MissingSystemTables_API;

        public DeliveryEnv_Validate_MissingSystemTables_CLI(DeliveryEnv_Validate_MissingSystemTables_API deliveryEnv_Validate_MissingSystemTables_API)
        {
            _deliveryEnv_Validate_MissingSystemTables_API = deliveryEnv_Validate_MissingSystemTables_API;
        }

        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _deliveryEnv_Validate_MissingSystemTables_API.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public void Asserts(TestContext testContext)
        {
            _deliveryEnv_Validate_MissingSystemTables_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertConsoleOutTextByLines.AssertLineMessage(0, "> Run 'validate' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertErrorsTextByLines.AssertLineMessage(0, "The process complete with errors:", true);
            assertErrorsTextByLines.AssertLineMessage(1, "--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage(2, "SystemTables. Error: The table 'AutoVersionsDB.DBScriptsExecutionHistory' is not exist in the db", false);
            assertErrorsTextByLines.AssertLineMessage(3, "", true);
            assertErrorsTextByLines.AssertLineMessage(4, "The system tables has invalid structure. Please try to 'Set DB State by Virtual Execution'.", true);
        }



        public void Release(TestContext testContext)
        {
            _deliveryEnv_Validate_MissingSystemTables_API.Release(testContext);
        }


    }
}
