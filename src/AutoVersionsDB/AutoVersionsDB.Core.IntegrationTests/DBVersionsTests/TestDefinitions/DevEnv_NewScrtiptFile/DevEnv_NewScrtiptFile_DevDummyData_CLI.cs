using AutoVersionsDB;
using AutoVersionsDB.CLI;
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
    public class DevEnv_NewScrtiptFile_DevDummyData_CLI : TestDefinition<CLITestContext>
    {

        private readonly DevEnv_NewScrtiptFile_DevDummyData_API _devEnv_NewScrtiptFile_DevDummyData_API;

        public DevEnv_NewScrtiptFile_DevDummyData_CLI(DevEnv_NewScrtiptFile_DevDummyData_API devEnv_NewScrtiptFile_DevDummyData_API)
        {
            _devEnv_NewScrtiptFile_DevDummyData_API = devEnv_NewScrtiptFile_DevDummyData_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_devEnv_NewScrtiptFile_DevDummyData_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"new ddd -id={IntegrationTestsConsts.TestProjectId} -sn={DevEnv_NewScrtiptFile_DevDummyData_API.ScriptName1}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _devEnv_NewScrtiptFile_DevDummyData_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 3);

            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "new ddd").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);
            assertTextByLines.AssertLineMessage(CLITextResources.TheFileIsCreatedInfoMessage.Replace("[newFilePath]", _devEnv_NewScrtiptFile_DevDummyData_API.GetScriptFullPath_DevDummyData_scriptName1(testContext.ProjectConfig.DBConnectionInfo)), true);
        }



        public override void Release(CLITestContext testContext)
        {
            _devEnv_NewScrtiptFile_DevDummyData_API.Release(testContext);
        }
    }
}
