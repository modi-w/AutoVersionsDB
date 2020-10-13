using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_New_DevDummyData_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DeliveryEnv_NotAllowMethods_New_DevDummyData_API _deliveryEnv_New_DevDummyData_API;

        public DeliveryEnv_NotAllowMethods_New_DevDummyData_CLI(DeliveryEnv_NotAllowMethods_New_DevDummyData_API deliveryEnv_New_DevDummyData_API)
        {
            _deliveryEnv_New_DevDummyData_API = deliveryEnv_New_DevDummyData_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _deliveryEnv_New_DevDummyData_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"new ddd -id={IntegrationTestsConsts.TestProjectId} -sn={_deliveryEnv_New_DevDummyData_API.ScriptName1}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _deliveryEnv_New_DevDummyData_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertConsoleOutTextByLines.AssertLineMessage(0, "> Run 'new ddd' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertErrorsTextByLines.AssertLineMessage(0, "The process complete with errors:", true);
            assertErrorsTextByLines.AssertLineMessage(1, "--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage(2, "DeliveryEnvironment. Error: Could not run this command on Delivery Environment", false);
            assertErrorsTextByLines.AssertLineMessage(3, "", true);
            assertErrorsTextByLines.AssertLineMessage(4, "Could not run this command on Delivery Environment", true);


        }


        public override void Release(DBVersionsTestContext testContext)
        {
            _deliveryEnv_New_DevDummyData_API.Release(testContext);
        }
    }
}
