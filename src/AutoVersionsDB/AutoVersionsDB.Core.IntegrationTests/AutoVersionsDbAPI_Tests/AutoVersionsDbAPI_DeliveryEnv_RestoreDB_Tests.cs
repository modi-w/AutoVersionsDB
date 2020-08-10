using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.Core.ProcessSteps;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    public class AutoVersionsDbAPI_DeliveryEnv_RestoreDB_Tests : AutoVersionsDbAPI_TestsBase
    {


        [Test]
        public void RestoreDB_SyncDB([ValueSource("ProjectConfigItemArray_DeliveryEnv_ScriptError")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            RemoveArtifactTempFolder(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.SyncDB(projectConfig, null);

            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            AssertRestore(projectConfig, dbBackupFileFileFullPath, processTrace);

        }

        //Comment: SetDBToSpecificState cannot run on delivery environment
        //Comment: RecreateDBFromScratch cannot run on delivery environment



        protected void AssertRestore(ProjectConfigItem projectConfig, string orginalDBBackupFilePathForTheTest, ProcessTrace processTrace)
        {
            Assert.That(processTrace.HasError);

            bool isRestoreExecuted = 
                processTrace
                .StatesHistory.Any(e => !string.IsNullOrWhiteSpace(e.StepName)
                                                        && e.StepName.StartsWith(RestoreDatabaseStep.StepNameStr));

            Assert.That(isRestoreExecuted, Is.EqualTo(true));

            string tempBackupFileToCompare = Path.Combine(FileSystemHelpers.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder), $"TempBackupFileToCompare_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}");

            using (IDBCommands dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0))
            {
                using (IDBBackupRestoreCommands dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, 0))
                {
                    dbBackupRestoreCommands.CreateDbBackup(tempBackupFileToCompare, dbCommands.GetDataBaseName());
                }
            }

            FileInfo fiOrginalDBBackupFilePathForTheTest = new FileInfo(orginalDBBackupFilePathForTheTest);
            FileInfo fiTempBackupFileToCompare = new FileInfo(tempBackupFileToCompare);


            //Comment: this check is not work because the original bak files was backup on diffrent sql server


            //  Assert.That(fiOrginalDBBackupFilePathForTheTest.Length, Is.EqualTo(fiTempBackupFileToCompare.Length));
        }



    }
}
