using AutoVersionsDB;
using AutoVersionsDB.CLI;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.Validators;
using AutoVersionsDB.Core.IntegrationTests;


using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_NotAollowMethods
{
    public class DeliveryEnv_NotAllowMethods_New_DevDummyData_CLI : TestDefinition<CLITestContext>
    {

        private readonly DeliveryEnv_NotAllowMethods_New_DevDummyData_API _deliveryEnv_New_DevDummyData_API;

        public DeliveryEnv_NotAllowMethods_New_DevDummyData_CLI(DeliveryEnv_NotAllowMethods_New_DevDummyData_API deliveryEnv_New_DevDummyData_API)
        {
            _deliveryEnv_New_DevDummyData_API = deliveryEnv_New_DevDummyData_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_deliveryEnv_New_DevDummyData_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"new ddd -id={IntegrationTestsConsts.TestProjectId} -sn={DeliveryEnv_NotAllowMethods_New_DevDummyData_API.ScriptName1}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _deliveryEnv_New_DevDummyData_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 1);
            assertConsoleOutTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "new ddd").Replace("[args]", "IntegrationTestProject"), true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError, 5);
            assertErrorsTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteWithErrors, true);
            assertErrorsTextByLines.AssertLineMessage("--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage($"{CheckDeliveryEnvValidator.Name}. Error: {CoreTextResources.DeliveryEnvErrorMessage}", false);
            assertErrorsTextByLines.AssertLineMessage("", true);
            assertErrorsTextByLines.AssertLineMessage(CoreTextResources.DeliveryEnvErrorMessage, true);


        }


        public override void Release(CLITestContext testContext)
        {
            _deliveryEnv_New_DevDummyData_API.Release(testContext);
        }
    }
}
