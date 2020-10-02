using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual
{
    public class DeliveryEnv_Virtual_CLI : ITestDefinition
    {

        private readonly DeliveryEnv_Virtual_API _deliveryEnv_Virtual_API;

        public DeliveryEnv_Virtual_CLI(DeliveryEnv_Virtual_API deliveryEnv_Virtual_API)
        {
            _deliveryEnv_Virtual_API = deliveryEnv_Virtual_API;
        }

        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _deliveryEnv_Virtual_API.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public void Act(TestContext testContext)
        {
            if (testContext.DBBackupFileType == DBBackupFileType.MiddleState)
            {
                AutoVersionsDbAPI.CLIRun($"virtual -id={IntegrationTestsConsts.TestProjectId} -t={IntegrationTestsConsts.TargetStateFile_FinalState}");
            }
            else
            {
                AutoVersionsDbAPI.CLIRun($"virtual -id={IntegrationTestsConsts.TestProjectId} -t={IntegrationTestsConsts.TargetStateFile_MiddleState}");
            }
        }


        public void Asserts(TestContext testContext)
        {
            _deliveryEnv_Virtual_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'virtual' for 'IntegrationTestProject'");
            assertTextByLines.AssertLineMessage(1, "The process complete successfully");
        }

    }
}
