using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using NUnit.Framework;
using System.IO;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    public class AutoVersionsDbAPI_DevEnv_SetDBStateByVirtualExecution_Tests : AutoVersionsDbAPI_TestsBase
    {

        [Test]
        public void DBStateInEmptyState_And_SetDBToMiddleState__Should_StillBeInEmptyState_ButMarkVirtuallyAsInMiddleState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_EmptyDB_ExceptSystemTables.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SetDBStateByVirtualExecution(projectConfig.Code, c_targetStateFile_MiddleState, null);

            //Assert
            assertProccessErrors(processResults.Trace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInEmptyStateExceptSystemTables(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
            assertThatDbExecutedFilesAreInMiddleState(projectConfig);
        }


        [Test]
        public void DBStateInMiddle_And_SetDBToFinalState__Should_StillBeInMiddleState_ButMarkVistuallyAsInFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SetDBStateByVirtualExecution(projectConfig.Code, c_targetStateFile_FinalState, null);

            //Assert
            assertProccessErrors(processResults.Trace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInMiddleState(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }

    }
}
