using AutoVersionsDB.Common;
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
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.SyncDB(projectConfig.ProjectCode, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            AssertRestore(projectConfig, dbBackupFileFileFullPath, processTrace);
        }

        [Test]
        public void RestoreDB_SetDBToSpecificState([ValueSource("ProjectConfigItemArray_DevEnv_ScriptError")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.SetDBToSpecificState(projectConfig.ProjectCode, c_targetStateFile_FinalState, false, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            AssertRestore(projectConfig, dbBackupFileFileFullPath, processTrace);
        }

        [Test]
        public void RestoreDB_RecreateDBFromScratch([ValueSource("ProjectConfigItemArray_DevEnv_ScriptError")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.RecreateDBFromScratch(projectConfig.ProjectCode, null, null);

            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            AssertRestore(projectConfig, dbBackupFileFileFullPath, processTrace);
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

            using (var dbCommands = _dbCommandsFactoryProvider.CreateDBCommand(projectConfig.DBTypeCode, projectConfig.ConnStr, 0).AsDisposable())
            {
                using (var dbBackupRestoreCommands = _dbCommandsFactoryProvider.CreateDBBackupRestoreCommands(projectConfig.DBTypeCode, projectConfig.ConnStrToMasterDB, 0).AsDisposable())
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
