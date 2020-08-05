using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System.IO;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    public class AutoVersionsDbAPI_DevEnv_SetDBToSpecificState_Tests : AutoVersionsDbAPI_TestsBase
    {
        [Test]
        public void DBStateInMiddle_And_SetDBToFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig, c_targetStateFile_FinalState, false, null);

            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }


        [Test]
        public void NextScriptHasVersionHigherThenTheLastFileVersion_NotSameDay__Should_SetDBToFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig, c_targetStateFile_FinalState, false, null);

            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }


        [Test]
        public void DBStateIsEmpty_And_SetDBToSpecificPoint_InTheFuture_NotFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_AfterRunInitStateScript.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig, c_targetStateFile_MiddleState, false, null);

            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInMiddleState(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }


        [Test]
        public void TargetScriptInHistory_IgnoreHistoryWarningIsFalse__ShouldHasTargetHistoryError([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig, c_targetStateFile_MiddleState, false, null);

            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processTrace.HasError);
            Assert.That(processTrace.ErrorCode == "IsTargetScriptFiletAlreadyExecuted");
        }

        [Test]
        public void TargetScriptInHistory_IgnoreHistoryWarningIsTrue__ShouldExecuteWithNoErrors([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig, c_targetStateFile_MiddleState, true, null);

            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInMiddleState(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }


    }
}
