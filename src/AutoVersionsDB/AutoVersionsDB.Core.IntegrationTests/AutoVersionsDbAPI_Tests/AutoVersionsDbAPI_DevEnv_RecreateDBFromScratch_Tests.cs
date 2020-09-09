using AutoVersionsDB.Common;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System.IO;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    public class AutoVersionsDbAPI_DevEnv_RecreateDBFromScratch_Tests : AutoVersionsDbAPI_TestsBase
    {


        [Test]
        public void DBStateIsEmpty__ShouldBeInFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_EmptyDB.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.RecreateDBFromScratch(projectConfig, null, null);


            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void DBStateInMiddle__ShouldBeInFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.RecreateDBFromScratch(projectConfig, c_targetStateFile_FinalState, null);

            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void NextScriptHasVersionHigherThenTheLastFileVersion_NotSameDay__ShouldBeInFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.RecreateDBFromScratch(projectConfig, null, null);

            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void DBStateIsAfterFinalState__ShouldBeInFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_AddDataAfterFinalState.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.RecreateDBFromScratch(projectConfig, null, null);

            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void DBStateIsAfterFinalState_TargetFileIsMiddleState__ShouldBeInMiddleState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.RecreateDBFromScratch(projectConfig,c_targetStateFile_MiddleState, null);

            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInMiddleState(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }

    }
}
