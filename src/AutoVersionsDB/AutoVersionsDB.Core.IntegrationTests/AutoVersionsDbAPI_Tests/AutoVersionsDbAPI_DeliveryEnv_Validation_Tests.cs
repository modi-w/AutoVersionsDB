using AutoVersionsDB.Common;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
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
            //Arrange
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                IsDevEnvironment = false
            };
      
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);



            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.ValidateProjectConfig(projectConfig.ProjectCode, null);


            //Assert
            List<StepNotificationState> notificationStatesHistory = processTrace.StatesHistory;
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "ProjectCodeNotEmpty"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "ConnStr"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBBackupFolderPath"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBTypeCode"));
            Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DeliveryArtifactFolderPath"));
        }


        [Test]
        public void DeliveryEnv_ProjectConfigValidate_Valid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            RemoveArtifactTempFolder(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.ValidateProjectConfig(projectConfig.ProjectCode, null);


            //Assert
            assertProccessErrors(processTrace);
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
        }


        [Test]
        public void DeliveryEnv_ValidateAll_Valid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            RemoveArtifactTempFolder(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.ValidateAll(projectConfig.ProjectCode, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertProccessErrors(processTrace);
        }



        [Test]
        public void DeliveryEnv_IsHistoryExecutedFilesChanged_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ChangedHistoryFiles_Incremental")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            RemoveArtifactTempFolder(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.ValidateAll(projectConfig.ProjectCode, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processTrace.HasError);
            Assert.That(processTrace.ErrorCode == "IsHistoryExecutedFilesChanged");
        }


        [Test]
        public void DeliveryEnv_ScriptsFilesAndDBExecutionHistoryIsMatch_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_MissingFile")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            RemoveArtifactTempFolder(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.ValidateAll(projectConfig.ProjectCode, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processTrace.HasError);
            Assert.That(processTrace.ErrorCode == "IsHistoryExecutedFilesChanged");
        }


        [Test]
        public void DeliveryEnv_TablesExist_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            RemoveArtifactTempFolder(projectConfig);
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_MissingSystemTables.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.ValidateAll(projectConfig.ProjectCode, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processTrace.HasError);
            Assert.That(processTrace.ErrorCode == "SystemTables");
        }


        //Comment: TargetStateScript is not relevant on delivery evnironment, so we dont need to test it


        [Test]
        public void DeliveryEnv_ValidateArtifactFile_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);

            RemoveArtifactTempFolder(projectConfig);
            projectConfig.DeliveryArtifactFolderPath += "_NotExistFolderSuffix";

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.ValidateAll(projectConfig.ProjectCode, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processTrace.HasError);
            Assert.That(processTrace.ErrorCode == "ArtifactFile");
        }





    }
}
