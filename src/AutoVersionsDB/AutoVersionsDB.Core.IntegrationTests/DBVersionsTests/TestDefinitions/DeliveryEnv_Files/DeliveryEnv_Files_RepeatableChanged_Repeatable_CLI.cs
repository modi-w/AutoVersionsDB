﻿using AutoVersionsDB;
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
    public class DeliveryEnv_Files_RepeatableChanged_Repeatable_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DeliveryEnv_Files_RepeatableChanged_API _files_RepeatableChanged_API;

        public DeliveryEnv_Files_RepeatableChanged_Repeatable_CLI(DeliveryEnv_Files_RepeatableChanged_API devEnv_Files_RepeatableChanged_API)
        {
            _files_RepeatableChanged_API = devEnv_Files_RepeatableChanged_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _files_RepeatableChanged_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"files repeatable -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _files_RepeatableChanged_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);

            assertTextByLines.AssertLineMessage(0, "> Run 'files repeatable' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);
            assertTextByLines.AssertLineMessage(2, "", true);
            assertTextByLines.AssertLineMessage(3, "++ Repeatable Scripts:", true);
            assertTextByLines.AssertLineMessage(4, "  Status   |  File", true);
            assertTextByLines.AssertLineMessage(5, "-------------------------------------------------------", true);
            assertTextByLines.AssertLineMessage(6, "   changed | rptScript_DataForLookupTable1.sql", true);
            assertTextByLines.AssertLineMessage(7, "   sync    | rptScript_DataForLookupTable2.sql", true);

        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _files_RepeatableChanged_API.Release(testContext);
        }

    }
}
