using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile
{
    public class DevEnv_NewScrtiptFile_Incremental_CLI : ITestDefinition
    {

        private readonly DevEnv_NewScrtiptFile_Incremental_API _devEnv_NewScrtiptFile_Incremental_API;

        public DevEnv_NewScrtiptFile_Incremental_CLI(DevEnv_NewScrtiptFile_Incremental_API devEnv_NewScrtiptFile_Incremental_API)
        {
            _devEnv_NewScrtiptFile_Incremental_API = devEnv_NewScrtiptFile_Incremental_API;
        }

        public TestContext Arrange(ProjectConfigItem projectConfig, DBBackupFileType dbBackupFileType, ScriptFilesStateType scriptFilesStateType)
        {
            TestContext testContext = _devEnv_NewScrtiptFile_Incremental_API.Arrange(projectConfig, dbBackupFileType, scriptFilesStateType);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public void Act(TestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"new incremental -id={IntegrationTestsConsts.TestProjectId} -sn={_devEnv_NewScrtiptFile_Incremental_API.ScriptName1}");
            AutoVersionsDBAPI.CLIRun($"new incremental -id={IntegrationTestsConsts.TestProjectId} -sn={_devEnv_NewScrtiptFile_Incremental_API.ScriptName2}");
        }


        public void Asserts(TestContext testContext)
        {
            _devEnv_NewScrtiptFile_Incremental_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
          
            assertTextByLines.AssertLineMessage(0, "> Run 'new incremental' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);
            assertTextByLines.AssertLineMessage(2, $"The file: '{_devEnv_NewScrtiptFile_Incremental_API.GetScriptFullPath_Incremental_scriptName1(testContext.ProjectConfig.DBConnectionInfo)}' is created.", true);

            assertTextByLines.AssertLineMessage(3, "> Run 'new incremental' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(4, "The process complete successfully", true);
            assertTextByLines.AssertLineMessage(5, $"The file: '{_devEnv_NewScrtiptFile_Incremental_API.GetScriptFullPath_Incremental_scriptName2(testContext.ProjectConfig.DBConnectionInfo)}' is created.", true);
        }

    }
}
