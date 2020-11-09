using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Files;
using System;
using System.Collections.Generic;
using System.Text;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Files
{
    public class DeliveryEnv_Files_RepeatableChanged_DevDummyData_CLI : TestDefinition<DBVersionsAPITestContext>
    {

        private readonly DeliveryEnv_Files_RepeatableChanged_API _files_RepeatableChanged_API;

        public DeliveryEnv_Files_RepeatableChanged_DevDummyData_CLI(DeliveryEnv_Files_RepeatableChanged_API devEnv_Files_RepeatableChanged_API)
        {
            _files_RepeatableChanged_API = devEnv_Files_RepeatableChanged_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _files_RepeatableChanged_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"files ddd -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _files_RepeatableChanged_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,2);

            assertTextByLines.AssertLineMessage("> Run 'files ddd' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _files_RepeatableChanged_API.Release(testContext);
        }

    }
}
