﻿using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using AutoVersionsDB.DB;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations
{
    public class DevEnv_Validate_MissingSystemTables_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_Validate_MissingSystemTables_API _devEnv_Validate_MissingSystemTables_API;

        public DevEnv_Validate_MissingSystemTables_CLI(DevEnv_Validate_MissingSystemTables_API devEnv_Validate_MissingSystemTables_API)
        {
            _devEnv_Validate_MissingSystemTables_API = devEnv_Validate_MissingSystemTables_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_Validate_MissingSystemTables_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_Validate_MissingSystemTables_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 1);
            assertConsoleOutTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "validate").Replace("[args]", "IntegrationTestProject"), true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError, 5);
            assertErrorsTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteWithErrors, true);
            assertErrorsTextByLines.AssertLineMessage("--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage($"{SystemTablesValidator.Name}. Error: {CoreTextResources.TableNotExistErrorMessage.Replace("[TableName]", DBCommandsConsts.DBScriptsExecutionHistoryFullTableName)}", false);
            assertErrorsTextByLines.AssertLineMessage("", true);
            assertErrorsTextByLines.AssertLineMessage(CoreTextResources.SystemTablesDevEnvInstructionsMessage, true);


        }


        public override void Release(CLITestContext testContext)
        {
            _devEnv_Validate_MissingSystemTables_API.Release(testContext);
        }

    }
}
