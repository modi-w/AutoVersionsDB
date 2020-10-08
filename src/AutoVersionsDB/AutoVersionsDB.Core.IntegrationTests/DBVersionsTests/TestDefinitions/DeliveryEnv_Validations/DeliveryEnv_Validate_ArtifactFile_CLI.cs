﻿using AutoVersionsDB;
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
    public class DeliveryEnv_Validate_ArtifactFile_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DeliveryEnv_Validate_ArtifactFile_API _deliveryEnv_Validate_ArtifactFile_API;

        public DeliveryEnv_Validate_ArtifactFile_CLI(DeliveryEnv_Validate_ArtifactFile_API deliveryEnv_Validate_ArtifactFile_API)
        {
            _deliveryEnv_Validate_ArtifactFile_API = deliveryEnv_Validate_ArtifactFile_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _deliveryEnv_Validate_ArtifactFile_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _deliveryEnv_Validate_ArtifactFile_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertConsoleOutTextByLines.AssertLineMessage(0, "> Run 'validate' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertErrorsTextByLines.AssertLineMessage(0, "The process complete with errors:", true);
            assertErrorsTextByLines.AssertLineMessage(1, "--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage(2, "ArtifactFile. Error: Delivery Foder not exist", false);
            assertErrorsTextByLines.AssertLineMessage(3, "", true);
            assertErrorsTextByLines.AssertLineMessage(4, "Artifact File not exist", true);


        }


        public override void Release(DBVersionsTestContext testContext)
        {
            _deliveryEnv_Validate_ArtifactFile_API.Release(testContext);
        }
    }
}
