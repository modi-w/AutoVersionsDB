using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.DBVersions.Processes.ActionSteps;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.NotificationableEngine;
using Moq;
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
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SyncDB(projectConfig.Id, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            AssertRestore(projectConfig, dbBackupFileFileFullPath, processResults.Trace);
        }

        [Test]
        public void RestoreDB_SetDBToSpecificState([ValueSource("ProjectConfigItemArray_DevEnv_ScriptError")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig.Id, c_targetStateFile_FinalState, false, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            AssertRestore(projectConfig, dbBackupFileFileFullPath, processResults.Trace);
        }

        [Test]
        public void RestoreDB_RecreateDBFromScratch([ValueSource("ProjectConfigItemArray_DevEnv_ScriptError")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessResults processResults = AutoVersionsDbAPI.RecreateDBFromScratch(projectConfig.Id, null, null);

            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            AssertRestore(projectConfig, dbBackupFileFileFullPath, processResults.Trace);
        }







        protected void AssertRestore(ProjectConfigItem projectConfig, string orginalDBBackupFilePathForTheTest, ProcessTrace processTrace)
        {
            Assert.That(processTrace.HasError);

            bool isRestoreExecuted =
                processTrace
                .StatesHistory.Any(e => e.InternalStepNotificationState != null
                                        && !string.IsNullOrWhiteSpace(e.InternalStepNotificationState.StepName)
                                        && e.InternalStepNotificationState.StepName.StartsWith(RestoreDatabaseStep.StepNameStr));


            Assert.That(isRestoreExecuted, Is.EqualTo(true));

            string tempBackupFileToCompare = Path.Combine(FileSystemPathUtils.ParsePathVaribles(IntegrationTestsSetting.DBBackupBaseFolder), $"TempBackupFileToCompare_{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-fff")}");

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBConnectionInfo).AsDisposable())
            {
                using (var dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBConnectionInfo).AsDisposable())
                {
                    dbBackupRestoreCommands.Instance.CreateDbBackup(tempBackupFileToCompare, dbCommands.Instance.GetDataBaseName());
                }
            }

            FileInfo fiOrginalDBBackupFilePathForTheTest = new FileInfo(orginalDBBackupFilePathForTheTest);
            FileInfo fiTempBackupFileToCompare = new FileInfo(tempBackupFileToCompare);


            //Comment: this check is not work because the original bak files was backup on diffrent sql server


            //    Assert.That(fiOrginalDBBackupFilePathForTheTest.Length, Is.EqualTo(fiTempBackupFileToCompare.Length));
        }



    }
}
