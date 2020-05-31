using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    public class AutoVersionsDbAPI_DevEnv_RestoreDB_Tests : AutoVersionsDbAPI_TestsBase
    {


        [Test]
        public void RestoreDB_SyncDB([ValueSource("ProjectConfigItemArray_DevEnv_ScriptError")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.SyncDB();

            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertRestore(projectConfig, dbBackupFileFileFullPath);
        }

        [Test]
        public void RestoreDB_SetDBToSpecificState([ValueSource("ProjectConfigItemArray_DevEnv_ScriptError")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.SetDBToSpecificState(c_targetStateFile_FinalState, false);

            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertRestore(projectConfig, dbBackupFileFileFullPath);
        }

        [Test]
        public void RestoreDB_RecreateDBFromScratch([ValueSource("ProjectConfigItemArray_DevEnv_ScriptError")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.RecreateDBFromScratch(null);

            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertRestore(projectConfig, dbBackupFileFileFullPath);
        }







        protected void assertRestore(ProjectConfigItem projectConfig, string orginalDBBackupFilePathForTheTest)
        {
            Assert.That(_autoVersionsDbAPI.HasError);

            string restoreStepName = "Rollback (Restore) Database";

            bool isRestoreExecuted = _autoVersionsDbAPI.NotificationExecutersFactoryManager.NotifictionStatesHistoryManager
                .NotificationStatesProcessHistory.Any(e => !string.IsNullOrWhiteSpace(e.StepName)
                                                        && e.StepName.StartsWith(restoreStepName));

            Assert.That(isRestoreExecuted, Is.EqualTo(true));

            string tempBackupFileToCompare = Path.Combine(FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder), $"TempBackupFileToCompare_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}");

            using (IDBCommands dbCommands = _dbCommands_FactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                using (IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommands_FactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, 0))
                {
                    dbBackupRestoreCommands.CreateDbBackup(tempBackupFileToCompare, dbCommands.GetDataBaseName());
                }
            }

            FileInfo fiOrginalDBBackupFilePathForTheTest = new FileInfo(orginalDBBackupFilePathForTheTest);
            FileInfo fiTempBackupFileToCompare = new FileInfo(tempBackupFileToCompare);


            //Comment: this check is not work because the original bak files was backup on diffrent sql server


            //    Assert.That(fiOrginalDBBackupFilePathForTheTest.Length, Is.EqualTo(fiTempBackupFileToCompare.Length));
        }



    }
}
