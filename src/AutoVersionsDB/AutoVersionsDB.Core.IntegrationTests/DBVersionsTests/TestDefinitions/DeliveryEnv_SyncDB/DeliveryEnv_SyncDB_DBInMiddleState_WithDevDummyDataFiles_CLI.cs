using AutoVersionsDB.Core.ConfigProjects;



using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_CLI : TestDefinition<DBVersionsAPITestContext>
    {

        private readonly DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_API _deliveryEnv_SyncDB_API;

        public DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_CLI(DeliveryEnv_SyncDB_DBInMiddleState_WithDevDummyDataFiles_API deliveryEnv_SyncDB_API)
        {
            _deliveryEnv_SyncDB_API = deliveryEnv_SyncDB_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _deliveryEnv_SyncDB_API.Arrange(testArgs);
            
            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(DBVersionsAPITestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"sync -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(this.GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut,2);
            assertTextByLines.AssertLineMessage("> Run 'sync' for 'IntegrationTestProject'",true);
            assertTextByLines.AssertLineMessage("The process complete successfully", true);
        }



        public override void Release(DBVersionsAPITestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Release(testContext);
        }

    }
}
