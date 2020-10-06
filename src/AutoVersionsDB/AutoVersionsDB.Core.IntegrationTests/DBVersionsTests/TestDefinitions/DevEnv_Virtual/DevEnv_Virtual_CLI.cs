using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual
{
    public class DevEnv_Virtual_CLI : ITestDefinition
    {

        private readonly DevEnv_Virtual_API _devEnv_Virtual_API;

        public DevEnv_Virtual_CLI(DevEnv_Virtual_API devEnv_Virtual_API)
        {
            _devEnv_Virtual_API = devEnv_Virtual_API;
        }

        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _devEnv_Virtual_API.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public void Act(TestContext testContext)
        {
            if (testContext.DBBackupFileType == DBBackupFileType.MiddleState)
            {
                AutoVersionsDBAPI.CLIRun($"virtual -id={IntegrationTestsConsts.TestProjectId} -t={IntegrationTestsConsts.TargetStateFile_FinalState}");
            }
            else
            {
                AutoVersionsDBAPI.CLIRun($"virtual -id={IntegrationTestsConsts.TestProjectId} -t={IntegrationTestsConsts.TargetStateFile_MiddleState}");
            }
        }


        public void Asserts(TestContext testContext)
        {
            _devEnv_Virtual_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'virtual' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);
        }


        public void Release(TestContext testContext)
        {
            _devEnv_Virtual_API.Release(testContext);
        }

    }
}
