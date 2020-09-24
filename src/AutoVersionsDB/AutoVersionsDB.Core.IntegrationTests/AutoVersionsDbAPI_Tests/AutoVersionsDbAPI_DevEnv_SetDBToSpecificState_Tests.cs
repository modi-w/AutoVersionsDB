﻿using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
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
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig.Id, c_targetStateFile_FinalState, false, null);

            //Assert
            assertProccessErrors(processResults.Trace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }


        [Test]
        public void NextScriptHasVersionHigherThenTheLastFileVersion_NotSameDay__Should_SetDBToFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig.Id, c_targetStateFile_FinalState, false, null);

            //Assert
            assertProccessErrors(processResults.Trace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }


        [Test]
        public void DBStateIsEmpty_And_SetDBToSpecificPoint_InTheFuture_NotFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_AfterRunInitStateScript.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig.Id, c_targetStateFile_MiddleState, false, null);

            //Assert
            assertProccessErrors(processResults.Trace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInMiddleState(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }


        [Test]
        public void TargetScriptInHistory_IgnoreHistoryWarningIsFalse__ShouldHasTargetHistoryError([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig.Id, c_targetStateFile_MiddleState, false, null);

            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processResults.Trace.HasError);
            Assert.That(processResults.Trace.ErrorCode == "IsTargetScriptFiletAlreadyExecuted");
        }

        [Test]
        public void TargetScriptInHistory_IgnoreHistoryWarningIsTrue__ShouldExecuteWithNoErrors([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig.Id, c_targetStateFile_MiddleState, true, null);

            //Assert
            assertProccessErrors(processResults.Trace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInMiddleState(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }


    }
}
