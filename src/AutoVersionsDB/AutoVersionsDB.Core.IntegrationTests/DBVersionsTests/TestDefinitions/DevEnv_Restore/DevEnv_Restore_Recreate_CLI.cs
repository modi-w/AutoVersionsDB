﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Restore
{
    public class DevEnv_Restore_Recreate_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_Restore_Recreate_API _devEnv_Restore_Recreate_API;

        public DevEnv_Restore_Recreate_CLI(DevEnv_Restore_Recreate_API devEnv_Restore_Recreate_API)
        {
            _devEnv_Restore_Recreate_API = devEnv_Restore_Recreate_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _devEnv_Restore_Recreate_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"recreate -id={IntegrationTestsConsts.TestProjectId}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _devEnv_Restore_Recreate_API.Asserts(testContext);

            AssertTextByLines assertConsoleOutTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertConsoleOutTextByLines.AssertLineMessage(0, "> Run 'recreate' for 'IntegrationTestProject'", true);

            AssertTextByLines assertErrorsTextByLines = new AssertTextByLines(GetType().Name, "ConsoleError", testContext.ConsoleError);
            assertErrorsTextByLines.AssertLineMessage(0, "The process complete with errors:", true);
            assertErrorsTextByLines.AssertLineMessage(1, "--------------------------------", true);
            assertErrorsTextByLines.AssertLineMessage(2, "incScript_2020-03-02.100_CreateTransTable1.sql 0% (0/1) -> Execute Script Block. Error: System.Exception: Error Message: 'Column, parameter, or variable #3: Cannot find data type nvarcharaaaa.', Script: ", false);
        }


        public override void Release(DBVersionsTestContext testContext)
        {
            _devEnv_Restore_Recreate_API.Release(testContext);
        }
    }
}
