﻿using AutoVersionsDB;
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
    public class DeliveryEnv_Validate_NewProject_CLI : TestDefinition<CLITestContext>
    {

        private readonly DeliveryEnv_Validate_NewProject_API _DeliveryEnv_Validate_NewProject_API;

        public DeliveryEnv_Validate_NewProject_CLI(DeliveryEnv_Validate_NewProject_API DeliveryEnv_Validate_NewProject_API)
        {
            _DeliveryEnv_Validate_NewProject_API = DeliveryEnv_Validate_NewProject_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_DeliveryEnv_Validate_NewProject_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _DeliveryEnv_Validate_NewProject_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 1);
            assertConsoleOutTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "validate").Replace("[args]", "IntegrationTestProject"), true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError, 2);
            assertErrorsTextByLines.AssertLineMessage("Welcome!!! This appear to be a new project.", true);
            assertErrorsTextByLines.AssertLineMessage("1) Copy the artifact file that deployed from your dev environment >> 2) Run 'Virtual' to set the current DB state related to the scripts file >> 3) Run 'Sync' for executing the rest of the scripts files", true);


        }


        public override void Release(CLITestContext testContext)
        {
            _DeliveryEnv_Validate_NewProject_API.Release(testContext);
        }

    }
}
