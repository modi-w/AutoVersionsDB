using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
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
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_EmptyDB.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.RecreateDBFromScratch(null);


            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void DBStateInMiddle__ShouldBeInFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.RecreateDBFromScratch(c_targetStateFile_FinalState);

            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void NextScriptHasVersionHigherThenTheLastFileVersion_NotSameDay__ShouldBeInFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.RecreateDBFromScratch(null);

            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void DBStateIsAfterFinalState__ShouldBeInFinalState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_AddDataAfterFinalState.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.RecreateDBFromScratch(null);

            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInFinalState_DevEnv(projectConfig);
            assertThatAllFilesInFolderExistWithTheSameHashInTheDb_FinalState(projectConfig);
        }


        [Test]
        public void DBStateIsAfterFinalState_TargetFileIsMiddleState__ShouldBeInMiddleState([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.RecreateDBFromScratch(c_targetStateFile_MiddleState);

            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertThatTheProcessBackupDBFileEualToTheOriginalRestoreDBFile(projectConfig, dbBackupFileFileFullPath);
            assertDbInMiddleState(projectConfig);
            assertThatAllFilesInTheDbExistWithTheSameHashInTheFolder(projectConfig);
        }

    }
}
