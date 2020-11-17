using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile
{
    public class DevEnv_NewScrtiptFile_Repeatable_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_NewScrtiptFile_Repeatable_API _devEnv_NewScrtiptFile_Repeatable_API;

        public DevEnv_NewScrtiptFile_Repeatable_CLI(DevEnv_NewScrtiptFile_Repeatable_API devEnv_NewScrtiptFile_Repeatable_API)
        {
            _devEnv_NewScrtiptFile_Repeatable_API = devEnv_NewScrtiptFile_Repeatable_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_NewScrtiptFile_Repeatable_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"new repeatable -id={IntegrationTestsConsts.TestProjectId} -sn={_devEnv_NewScrtiptFile_Repeatable_API.ScriptName1}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_NewScrtiptFile_Repeatable_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 3);

            assertTextByLines.AssertLineMessage("> Run 'new repeatable' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);
            assertTextByLines.AssertLineMessage($"The file: '{_devEnv_NewScrtiptFile_Repeatable_API.GetScriptFullPath_Repeatable_scriptName1(testContext.ProjectConfig.DBConnectionInfo)}' is created.", true);
        }



        public override void Release(CLITestContext testContext)
        {
            _devEnv_NewScrtiptFile_Repeatable_API.Release(testContext);
        }

    }
}
