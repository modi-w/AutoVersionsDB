using AutoVersionsDB.Core.Common.CLI;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public class DeliveryEnv_SyncDB_DBInMiddleState_API : TestDefinition
    {

        public override TestContext Arrange(ProjectConfigItem projectConfig)
        {
            TestContext testContext = new TestContext(projectConfig);

            NinjectUtils_IntegrationTests.MockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(testContext.ProjectConfig);
            FoldersHelper.RemoveArtifactTempFolder(testContext.ProjectConfig);
            DBHelper.RestoreDB(testContext.ProjectConfig, DBBackupFileType.MiddleState);
            testContext.NumOfConnectionsBefore = DBHelper.GetNumOfOpenConnection(testContext.ProjectConfig);

            return testContext;
        }

        public override void Act(TestContext testContext)
        {
            testContext.ProcessResults = AutoVersionsDbAPI.SyncDB(testContext.ProjectConfig.Id, null);
        }


        public override void Assert(TestContext testContext)
        {
            AssertsHelpers.AssertProccessErrors(testContext.ProcessResults.Trace);
            AssertsHelpers.AssertNumOfOpenDbConnection(testContext.ProjectConfig, testContext.NumOfConnectionsBefore);
            AssertsHelpers.AssertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(testContext.ProjectConfig, DBBackupFileType.MiddleState);
            DBHelper.AssertDbInFinalState_DeliveryEnv(testContext.ProjectConfig);
            DBHelper.AssertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(testContext.ProjectConfig);
        }
    }


    public class DeliveryEnv_SyncDB_DBInMiddleState_CLI : DeliveryEnv_SyncDB_DBInMiddleState_API
    {
        protected readonly IConsoleProcessMessages ConsoleProcessMessages;

        public DeliveryEnv_SyncDB_DBInMiddleState_CLI(IConsoleProcessMessages consoleProcessMessages)
        {
            ConsoleProcessMessages = consoleProcessMessages;

        }


        public override void Act(TestContext testContext)
        {
            NinjectUtils_IntegrationTests.MockConsoleProcessMessages
                .Setup(m => m.ProcessComplete(It.IsAny<ProcessResults>()))
                .Callback<ProcessResults>((processResults) =>
                {
                    testContext.ProcessResults = processResults;
                });

            AutoVersionsDbAPI.CLIRun($"sync -id={IntegrationTestsSetting.TestProjectId}");


        }
    }
}
