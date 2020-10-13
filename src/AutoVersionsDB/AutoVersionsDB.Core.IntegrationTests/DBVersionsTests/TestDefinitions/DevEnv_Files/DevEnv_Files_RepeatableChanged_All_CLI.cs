﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_NewScrtiptFile;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Files
{
    public class DevEnv_Files_RepeatableChanged_All_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_Files_RepeatableChanged_API _files_RepeatableChanged_API;

        public DevEnv_Files_RepeatableChanged_All_CLI(DevEnv_Files_RepeatableChanged_API devEnv_Files_RepeatableChanged_API)
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
            AutoVersionsDBAPI.CLIRun($"files -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _files_RepeatableChanged_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);

            assertTextByLines.AssertLineMessage(0, "> Run 'files' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);
            assertTextByLines.AssertLineMessage(2, "", true);
            assertTextByLines.AssertLineMessage(3, "++ Incremental Scripts:", true);
            assertTextByLines.AssertLineMessage(4, "  Status   |  File", true);
            assertTextByLines.AssertLineMessage(5, "-------------------------------------------------------", true);
            assertTextByLines.AssertLineMessage(6, "   sync    | incScript_2020-02-25.100_initState.sql", true);
            assertTextByLines.AssertLineMessage(7, "   sync    | incScript_2020-02-25.101_CreateLookupTable1.sql", true);
            assertTextByLines.AssertLineMessage(8, "   sync    | incScript_2020-02-25.102_CreateLookupTable2.sql", true);
            assertTextByLines.AssertLineMessage(9, "   sync    | incScript_2020-03-02.100_CreateTransTable1.sql", true);
            assertTextByLines.AssertLineMessage(10, "   sync    | incScript_2020-03-02.101_CreateInvoiceTable1.sql", true);
            assertTextByLines.AssertLineMessage(11, "", true);
            assertTextByLines.AssertLineMessage(12, "++ Repeatable Scripts:", true);
            assertTextByLines.AssertLineMessage(13, "  Status   |  File", true);
            assertTextByLines.AssertLineMessage(14, "-------------------------------------------------------", true);
            assertTextByLines.AssertLineMessage(15, "   changed | rptScript_DataForLookupTable1.sql", true);
            assertTextByLines.AssertLineMessage(16, "   sync    | rptScript_DataForLookupTable2.sql", true);
            assertTextByLines.AssertLineMessage(17, "", true);
            assertTextByLines.AssertLineMessage(18, "++ DevDummyData Scripts:", true);
            assertTextByLines.AssertLineMessage(19, "  Status   |  File", true);
            assertTextByLines.AssertLineMessage(20, "-------------------------------------------------------", true);
            assertTextByLines.AssertLineMessage(21, "   sync    | dddScript_DataForInvoiceTable1.sql", true);
            assertTextByLines.AssertLineMessage(22, "   changed | dddScript_DataForTransTable1.sql", true);
        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _files_RepeatableChanged_API.Release(testContext);
        }

    }
}
