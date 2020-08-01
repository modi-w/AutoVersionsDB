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
    public class AutoVersionsDbAPI_DeliveryEnv_Validation_Tests : AutoVersionsDbAPI_TestsBase
    {
        [Test]
        public void DeliveryEnv_ProjectConfigValidate_NotValid()
        {
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                IsDevEnvironment = false
            };


            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);


            //Act
            _autoVersionsDbAPI.ValidateProjectConfig();


            //Assert
            List<NotificationStateItem> notificationStatesHistory = _autoVersionsDbAPI.NotificationExecutersFactoryManager.NotifictionStatesHistory.NotificationStatesProcessHistory;
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "ProjectName"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "ConnStr"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBBackupFolderPath"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBTypeCode"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DeliveryArtifactFolderPath"));
        }


        [Test]
        public void DeliveryEnv_ProjectConfigValidate_Valid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.ValidateProjectConfig();


            //Assert
            assertProccessErrors();
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
        }


        [Test]
        public void DeliveryEnv_ValidateAll_Valid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            _autoVersionsDbAPI.ValidateAll();


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertProccessErrors();
        }



        [Test]
        public void DeliveryEnv_IsHistoryExecutedFilesChanged_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ChangedHistoryFiles_Incremental")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
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
        public void DeliveryEnv_ScriptsFilesAndDBExecutionHistoryIsMatch_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_MissingFile")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
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
        public void DeliveryEnv_TablesExist_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
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


        //Comment: TargetStateScript is not relevant on delivery evnironment, so we dont need to test it


        [Test]
        public void DeliveryEnv_ValidateArtifactFile_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            projectConfig.DeliveryArtifactFolderPath += "_NotExistFolderSuffix";
            _autoVersionsDbAPI.SetProjectConfigItem(projectConfig);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            _autoVersionsDbAPI.ValidateAll();


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(_autoVersionsDbAPI.HasError);
            Assert.That(_autoVersionsDbAPI.ErrorCode == "ArtifactFile");
        }





    }
}
