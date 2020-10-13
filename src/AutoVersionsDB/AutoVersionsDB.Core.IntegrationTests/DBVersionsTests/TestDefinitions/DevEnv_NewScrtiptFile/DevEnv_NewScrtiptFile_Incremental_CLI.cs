using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile
{
    public class DevEnv_NewScrtiptFile_Incremental_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_NewScrtiptFile_Incremental_API _devEnv_NewScrtiptFile_Incremental_API;

        public DevEnv_NewScrtiptFile_Incremental_CLI(DevEnv_NewScrtiptFile_Incremental_API devEnv_NewScrtiptFile_Incremental_API)
        {
            _devEnv_NewScrtiptFile_Incremental_API = devEnv_NewScrtiptFile_Incremental_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _devEnv_NewScrtiptFile_Incremental_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"new incremental -id={IntegrationTestsConsts.TestProjectId} -sn={_devEnv_NewScrtiptFile_Incremental_API.ScriptName1}");
            AutoVersionsDBAPI.CLIRun($"new incremental -id={IntegrationTestsConsts.TestProjectId} -sn={_devEnv_NewScrtiptFile_Incremental_API.ScriptName2}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
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



        public override void Release(DBVersionsTestContext testContext)
        {
            _devEnv_NewScrtiptFile_Incremental_API.Release(testContext);
        }

    }
}
