﻿using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_Deploy_CLI : TestDefinition<CLITestContext>
    {

        private readonly DeliveryEnv_NotAllowMethods_Deploy_API _deliveryEnv_NotAllowMethods_Deploy_API;

        public DeliveryEnv_NotAllowMethods_Deploy_CLI(DeliveryEnv_NotAllowMethods_Deploy_API deliveryEnv_NotAllowMethods_Deploy_API)
        {
            _deliveryEnv_NotAllowMethods_Deploy_API = deliveryEnv_NotAllowMethods_Deploy_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_deliveryEnv_NotAllowMethods_Deploy_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"deploy -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _deliveryEnv_NotAllowMethods_Deploy_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 1);
            assertConsoleOutTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "deploy").Replace("[args]", "IntegrationTestProject"), true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError, 5);
            assertErrorsTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteWithErrors, true);
            assertErrorsTextByLines.AssertLineMessage("--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage($"{CheckDeliveryEnvValidator.Name}. Error: Could not run this command on Delivery Environment", false);
            assertErrorsTextByLines.AssertLineMessage("", true);
            assertErrorsTextByLines.AssertLineMessage("Could not run this command on Delivery Environment", true);


        }


        public override void Release(CLITestContext testContext)
        {
            _deliveryEnv_NotAllowMethods_Deploy_API.Release(testContext);
        }
    }
}
