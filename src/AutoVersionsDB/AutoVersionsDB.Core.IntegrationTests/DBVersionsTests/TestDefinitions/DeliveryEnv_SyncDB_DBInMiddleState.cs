using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.Core.IntegrationTests.CLI;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public class DeliveryEnv_SyncDB_DBInMiddleState_API : ITestDefinition
    {
        private readonly DBVersionsTestDefinition _dbVersionsTest;
        private readonly ScriptFilesAsserts _scriptFilesAsserts;
        private readonly DBAsserts _dbAsserts;

        public DeliveryEnv_SyncDB_DBInMiddleState_API(DBVersionsTestDefinition dbVersionsTest,
                                                       ScriptFilesAsserts scriptFilesAsserts,
                                                       DBAsserts dbAsserts)
        {
            _dbVersionsTest = dbVersionsTest;
            _scriptFilesAsserts = scriptFilesAsserts;
            _dbAsserts = dbAsserts;
        }


        public TestContext Arrange(ProjectConfigItem projectConfig)
        {
            return _dbVersionsTest.Arrange(projectConfig);
        }

        public void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDbAPI.SyncDB(testContext.ProjectConfig.Id, null);
        }


        public void Asserts(TestContext testContext)
        {
            _dbVersionsTest.Asserts(testContext);

            _dbAsserts.AssertDbInFinalState_DeliveryEnv(GetType().Name, testContext.ProjectConfig.DBConnectionInfo);
            _scriptFilesAsserts.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(GetType().Name, testContext.ProjectConfig);
        }
    }


    public class DeliveryEnv_SyncDB_DBInMiddleState_CLI : ITestDefinition
    {

        private readonly DeliveryEnv_SyncDB_DBInMiddleState_API _deliveryEnv_SyncDB_DBInMiddleState_API;

        public DeliveryEnv_SyncDB_DBInMiddleState_CLI(DeliveryEnv_SyncDB_DBInMiddleState_API deliveryEnv_SyncDB_DBInMiddleState_API)
        {
            _deliveryEnv_SyncDB_DBInMiddleState_API = deliveryEnv_SyncDB_DBInMiddleState_API;
        }

        public TestContext Arrange(ProjectConfigItem projectConfig)
        {
            TestContext testContext = _deliveryEnv_SyncDB_DBInMiddleState_API.Arrange(projectConfig);

            MockObjectsProvider.SetProcessResultsToTestContext(testContext);
            MockObjectsProvider.SetConsoleOutputToTestContext(testContext);

            return testContext;
        }


        public void Act(TestContext testContext)
        {
            AutoVersionsDbAPI.CLIRun($"sync -id={IntegrationTestsConsts.TestProjectId}");
        }


        public void Asserts(TestContext testContext)
        {
            _deliveryEnv_SyncDB_DBInMiddleState_API.Asserts(testContext);

            AssertTextByLines assertTextByLines = new AssertTextByLines(this.GetType().Name, "FinalConsoleOut", testContext.FinalConsoleOut);
            assertTextByLines.AssertLineMessage(0, "> Run 'sync' for 'IntegrationTestProject'");
            assertTextByLines.AssertLineMessage(1, "The process complete successfully");
        }

    }
}
