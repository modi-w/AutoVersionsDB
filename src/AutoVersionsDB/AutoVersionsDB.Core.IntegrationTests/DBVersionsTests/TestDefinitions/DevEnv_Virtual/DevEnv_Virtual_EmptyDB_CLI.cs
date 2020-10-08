﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DevEnv_Virtual
{
    public class DevEnv_Virtual_EmptyDB_CLI : TestDefinition<DBVersionsTestContext>
    {

        private readonly DevEnv_Virtual_EmptyDB_API _deliveryEnv_Virtual_API;

        public DevEnv_Virtual_EmptyDB_CLI(DevEnv_Virtual_EmptyDB_API deliveryEnv_Virtual_API)
        {
            _deliveryEnv_Virtual_API = deliveryEnv_Virtual_API;
        }

        public override TestContext Arrange(TestArgs testArgs)
        {
            TestContext testContext = _deliveryEnv_Virtual_API.Arrange(testArgs);

            MockObjectsProvider.SetTestContextDataByMockCallbacks(testContext);

            return testContext;
        }


        public override void Act(DBVersionsTestContext testContext)
        {
            AutoVersionsDBAPI.CLIRun($"virtual -id={IntegrationTestsConsts.TestProjectId} -t={IntegrationTestsConsts.TargetStateFile_MiddleState}");
        }


        public override void Asserts(DBVersionsTestContext testContext)
        {
            _deliveryEnv_Virtual_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'virtual' for 'IntegrationTestProject'", true);
            assertTextByLines.AssertLineMessage(1, "The process complete successfully", true);
        }



        public override void Release(DBVersionsTestContext testContext)
        {
            _deliveryEnv_Virtual_API.Release(testContext);
        }

    }
}