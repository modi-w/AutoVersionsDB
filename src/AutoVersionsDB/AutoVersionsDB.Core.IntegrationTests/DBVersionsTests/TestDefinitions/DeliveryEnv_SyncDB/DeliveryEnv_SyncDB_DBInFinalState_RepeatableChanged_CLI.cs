﻿using AutoVersionsDB.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.TestContexts;
using AutoVersionsDB.Core.IntegrationTests.TestsUtils.CLI;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB
{
    public class DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_CLI : TestDefinition<CLITestContext>
    {

        private readonly DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API _deliveryEnv_SyncDB_API;

        public DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_CLI(DeliveryEnv_SyncDB_DBInFinalState_RepeatableChanged_API deliveryEnv_SyncDB_API)
        {
            _deliveryEnv_SyncDB_API = deliveryEnv_SyncDB_API;
        }

        public override ITestContext Arrange(TestArgs testArgs)
        {
            CLITestContext testContext = new CLITestContext(_deliveryEnv_SyncDB_API.Arrange(testArgs));

            MockObjectsProvider.SetTestContextDataByMockCallbacksForCLI(testContext);

            return testContext;
        }


        public override void Act(CLITestContext testContext)
        {
            CLIRunner.CLIRun($"sync -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(CLITestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Asserts(testContext);

            AssertTextByLines.AssertEmpty(GetType().Name, nameof(testContext.ConsoleError), testContext.ConsoleError);

            AssertTextByLines assertTextByLines = new AssertTextByLines(this.GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut, 2);
            assertTextByLines.AssertLineMessage(CLITextResources.StartProcessMessageWithArgs.Replace("[processName]", "sync").Replace("[args]", "IntegrationTestProject"), true);
            assertTextByLines.AssertLineMessage(CLITextResources.ProcessCompleteSuccessfully, true);
        }



        public override void Release(CLITestContext testContext)
        {
            _deliveryEnv_SyncDB_API.Release(testContext);
        }

    }
}
