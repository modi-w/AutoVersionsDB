﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
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

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertConsoleOutTextByLines.AssertLineMessage(0, "> Run 'validate' for 'IntegrationTestProject'", true);

            if (testContext.ScriptFilesStateType == ScriptFilesStateType.IncrementalChanged)
            {
                AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
                assertErrorsTextByLines.AssertLineMessage(0, "The process complete with errors:", true);
                assertErrorsTextByLines.AssertLineMessage(1, "--------------------------------", true);
                assertErrorsTextByLines.AssertLineMessage(2, "HistoryExecutedFilesChanged. Error: The following files changed: 'incScript_2020-02-25.102_CreateLookupTable2.sql'", false);
                assertErrorsTextByLines.AssertLineMessage(3, "", true);
                assertErrorsTextByLines.AssertLineMessage(4, "History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'", true);
            }
            else if (testContext.ScriptFilesStateType == ScriptFilesStateType.MissingFile)
            {
                AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
                assertErrorsTextByLines.AssertLineMessage(0, "The process complete with errors:", true);
                assertErrorsTextByLines.AssertLineMessage(1, "--------------------------------", true);
                assertErrorsTextByLines.AssertLineMessage(2, "HistoryExecutedFilesChanged. Error: The following files missing from the scripts folder: 'incScript_2020-02-25.102_CreateLookupTable2.sql'", false);
                assertErrorsTextByLines.AssertLineMessage(3, "", true);
                assertErrorsTextByLines.AssertLineMessage(4, "History executed files changed, please 'Recreate DB From Scratch' or 'Set DB State as Virtual Execution'", true);
            }
            else
            {
                throw new Exception("Invalid Test");
            }

        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _deliveryEnv_Validate_HistoryExecutedFilesMissing_API.Release(testContext);
        }


    }
}