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
    public class DeliveryEnv_Validate_ArtifactFile_CLI : ITestDefinition
    {

        private readonly DeliveryEnv_Validate_ArtifactFile_API _deliveryEnv_Validate_ArtifactFile_API;

        public DeliveryEnv_Validate_ArtifactFile_CLI(DeliveryEnv_Validate_ArtifactFile_API deliveryEnv_Validate_ArtifactFile_API)
        {
            _deliveryEnv_Validate_ArtifactFile_API = deliveryEnv_Validate_ArtifactFile_API;
        }

        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _deliveryEnv_Validate_ArtifactFile_API.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public void Act(TestContext testContext)
        {
            AutoVersionsDbAPI.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public void Asserts(TestContext testContext)
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

    }
}
