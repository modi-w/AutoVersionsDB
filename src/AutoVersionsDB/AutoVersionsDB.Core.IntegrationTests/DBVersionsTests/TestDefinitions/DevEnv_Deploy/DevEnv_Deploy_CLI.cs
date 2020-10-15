using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Deploy
{
    public class DevEnv_Deploy_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_Deploy_API _devEnv_Deploy_API;

        public DevEnv_Deploy_CLI(DevEnv_Deploy_API devEnv_Deploy_API)
        {
            _devEnv_Deploy_API = devEnv_Deploy_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _devEnv_Deploy_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"deploy -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _devEnv_Deploy_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,3);
            assertTextByLines.AssertLineMessage("> Run 'deploy' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);
            assertTextByLines.AssertLineMessage("Artifact file created: ", false,2);
            assertTextByLines.AssertLineMessage(@"Deploy\AutoVersionsDB.Tests.avdb'", false,2);
        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _devEnv_Deploy_API.Release(testContext);
        }
    }
}
