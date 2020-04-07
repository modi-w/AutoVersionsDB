﻿using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using NUnit.Framework;
using System.IO;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    public class AutoVersionsDbAPI_DeliveryEnv_SyncDB_Tests : AutoVersionsDbAPI_TestsBase
    {



        [Test]
        public void DBStateInMiddle___ShouldBeInFinalState_ShouldNotRunDevDummyData([ValueSource("ProjectConfigItemArray_DeliveryEnv_WithDevDummyDataFiles")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.SyncDB();

            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DeliveryEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void NextScriptHasVersionHigherThenTheLastFileVersion_NotSameDay__ShouldBeInFinalState([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.SyncDB();

            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DeliveryEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void RepeatableScriptsChanged__ShouldRunOnlyTheChangedScripts([ValueSource("ProjectConfigItemArray_DeliveryEnv_ChangedHistoryFiles_Repeatable")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
            restoreDB(projectConfig, dbBackupFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.SyncDB();


            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFullPath);
            assertDbInFinalState_DeliveryEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_RunAgainAfterRepetableFilesChanged(projectConfig);
        }

    }

}
