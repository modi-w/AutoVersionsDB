﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_Recreate_CLI : TestDefinition<DBVersionsAPITestContext>
    {

        private readonly DeliveryEnv_NotAllowMethods_Recreate_API _deliveryEnv_NotAllowMethods_Recreate_API;

        public DeliveryEnv_NotAllowMethods_Recreate_CLI(DeliveryEnv_NotAllowMethods_Recreate_API deliveryEnv_NotAllowMethods_Recreate_API)
        {
            _deliveryEnv_NotAllowMethods_Recreate_API = deliveryEnv_NotAllowMethods_Recreate_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _deliveryEnv_NotAllowMethods_Recreate_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"recreate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_NotAllowMethods_Recreate_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 1);
            assertConsoleOutTextByLines.AssertLineMessage("> Run 'recreate' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError, 5);
            assertErrorsTextByLines.AssertLineMessage("The process complete with errors:", true);
            assertErrorsTextByLines.AssertLineMessage("--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage("DeliveryEnvironment. Error: Could not run this command on Delivery Environment", false);
            assertErrorsTextByLines.AssertLineMessage("", true);
            assertErrorsTextByLines.AssertLineMessage("Could not run this command on Delivery Environment", true);


        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_NotAllowMethods_Recreate_API.Release(testContext);
        }

    }
}
