using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    public class AutoVersionsDbAPI_DevEnv_Validation_Tests : AutoVersionsDbAPI_TestsBase
    {
        [Test]
        public void DevEnv_ProjectConfigValidate_NotValid()
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                IsDevEnvironment = true
            };

            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);

            //Act
            _autoVersionsDbAPI.ValidateProjectConfig();


            //Assert
            Assert.That(_autoVersionsDbAPI.HasError);

            List<NotificationStateItem> notificationStatesHistory = _autoVersionsDbAPI.NotificationExecutersFactoryManager.NotifictionStatesHistory.NotificationStatesProcessHistory;
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "ProjectName"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "ConnStr"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBBackupFolderPath"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBTypeCode"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DevScriptsBaseFolder"));
        }


        [Test]
        public void DevEnv_ProjectConfigValidate_Valid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.ValidateProjectConfig();


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertProccessErrors();
        }


        [Test]
        public void DevEnv_ValidateAll_Valid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.ValidateAll();


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertProccessErrors();
        }



        [Test]
        public void DevEnv_IsHistoryExecutedFilesChanged_NotValid([ValueSource("ProjectConfigItemArray_DevEnv_ChangedHistoryFiles_Incremental")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);
       
            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            _autoVersionsDbAPI.ValidateAll();


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(_autoVersionsDbAPI.HasError);
            Assert.That(_autoVersionsDbAPI.ErrorCode == "IsHistoryExecutedFilesChanged");
        }


        [Test]
        public void DevEnv_ScriptsFilesAndDBExecutionHistoryIsMatch_NotValid([ValueSource("ProjectConfigItemArray_DevEnv_MissingFile")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);
         
            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            _autoVersionsDbAPI.ValidateAll();


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(_autoVersionsDbAPI.HasError);
            Assert.That(_autoVersionsDbAPI.ErrorCode == "IsHistoryExecutedFilesChanged");
        }


        [Test]
        public void DevEnv_TablesExist_NotValid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_MissingSystemTables.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            _autoVersionsDbAPI.ValidateAll();


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(_autoVersionsDbAPI.HasError);
            Assert.That(_autoVersionsDbAPI.ErrorCode == "SystemTables");
        }


        [Test]
        public void DevEnv_TargetStateScriptShouldNotBeHistorical_Valid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.ValdiateTargetStateAlreadyExecuted(c_targetStateFile_FinalState);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertProccessErrors();
        }

        [Test]
        public void DevEnv_TargetStateScriptShouldNotBeHistorical_NotValid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);
       
            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.ValdiateTargetStateAlreadyExecuted(c_targetStateFile_MiddleState);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(_autoVersionsDbAPI.HasError);
        }



    }
}
