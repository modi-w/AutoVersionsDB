using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API _deliveryEnv_SyncDB_API;

        public DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_CLI(DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API deliveryEnv_SyncDB_API)
        {
            _deliveryEnv_SyncDB_API = deliveryEnv_SyncDB_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _deliveryEnv_SyncDB_API.Arrange(testArgs);
            
            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"sync -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(this.GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'sync' for 'IntegrationTestProject'",true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);
        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Release(testContext);
        }

    }
}
