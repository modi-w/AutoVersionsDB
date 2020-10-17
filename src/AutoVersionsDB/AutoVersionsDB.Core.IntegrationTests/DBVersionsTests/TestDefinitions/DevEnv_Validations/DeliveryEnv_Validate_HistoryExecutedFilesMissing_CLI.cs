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
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations
{
    public class DevEnv_Validate_HistoryExecutedFilesMissing_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_Validate_HistoryExecutedFilesMissing_API _deliveryEnv_Validate_HistoryExecutedFilesMissing_API;

        public DevEnv_Validate_HistoryExecutedFilesMissing_CLI(DevEnv_Validate_HistoryExecutedFilesMissing_API deliveryEnv_Validate_HistoryExecutedFilesMissing_API)
        {
            _deliveryEnv_Validate_HistoryExecutedFilesMissing_API = deliveryEnv_Validate_HistoryExecutedFilesMissing_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _deliveryEnv_Validate_HistoryExecutedFilesMissing_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _deliveryEnv_Validate_HistoryExecutedFilesMissing_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,1);
            assertConsoleOutTextByLines.AssertLineMessage("> Run 'validate' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError,5);
            assertErrorsTextByLines.AssertLineMessage("The process complete with errors:", true);
            assertErrorsTextByLines.AssertLineMessage("--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage("HistoryExecutedFilesChanged. Error: The following files missing from the scripts folder: 'incScript_2020-02-25.102_CreateLookupTable2.sql'", false);
            assertErrorsTextByLines.AssertLineMessage("", true);
            assertErrorsTextByLines.AssertLineMessage("History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'", true);

        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _deliveryEnv_Validate_HistoryExecutedFilesMissing_API.Release(testContext);
        }


    }
}
