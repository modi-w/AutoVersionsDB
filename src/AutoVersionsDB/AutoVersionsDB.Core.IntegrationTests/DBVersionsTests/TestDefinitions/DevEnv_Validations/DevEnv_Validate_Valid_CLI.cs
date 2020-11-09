using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Validations
{
    public class DevEnv_Validate_Valid_CLI : TestDefinition<DBVersionsAPITestContext>
    {

        private readonly DevEnv_Validate_Valid_API _devEnv_Validate_API;

        public DevEnv_Validate_Valid_CLI(DevEnv_Validate_Valid_API devEnv_Validate_API)
        {
            _devEnv_Validate_API = devEnv_Validate_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _devEnv_Validate_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"validate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _devEnv_Validate_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,2);
            assertTextByLines.AssertLineMessage("> Run 'validate' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);

        }


        public override void Release(DBVersionsAPITestContext testContext)
        {
            _devEnv_Validate_API.Release(testContext);
        }

    }
}
