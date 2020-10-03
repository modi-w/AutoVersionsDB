using AutoVersionsDB.Helpers;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
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
        //[Test]
        //public void DeliveryEnv_ProjectConfigValidate_NotValid()
        //{
        //    //Arrange
        //    ProjectConfigItem projectConfig = new ProjectConfigItem()
        //    {
        //        Id = "aaa",
        //        DevEnvironment = false
        //    };

        //    _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);



        //    //Act
        //    ProcessResults processResults = AutoVersionsDbAPI.ValidateProjectConfig(projectConfig.Id, null);


        //    //Assert
        //    List<StepNotificationState> notificationStatesHistory = processResults.Trace.StatesHistory;
        //    //Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "IdMandatory"));
        //    Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBName"));
        //    Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBBackupFolderPath"));
        //    Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DBType"));
        //    Assert.That(notificationStatesHistory.Any(e => e.LowLevelErrorCode == "DeliveryArtifactFolderPath"));
        //}


        //[Test]
        //public void DeliveryEnv_ProjectConfigValidate_Valid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        //{
        //    //Arrange
        //    _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

        //    RemoveArtifactTempFolder(projectConfig);
        //    string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
        //    restoreDB(projectConfig, dbBackupFileFileFullPath);

        //    NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


        //    //Act
        //    ProcessResults processResults = AutoVersionsDbAPI.ValidateProjectConfig(projectConfig.Id, null);


        //    //Assert
        //    assertProccessErrors(processResults.Trace);
        //    assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
        //}


        //[Test]
        //public void DeliveryEnv_ValidateAll_Valid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        //{
        //    //Arrange
        //    _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

        //    RemoveArtifactTempFolder(projectConfig);
        //    string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
        //    restoreDB(projectConfig, dbBackupFileFileFullPath);

        //    NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


        //    //Act
        //    ProcessResults processResults = AutoVersionsDbAPI.ValidateDBVersions(projectConfig.Id, null);


        //    //Assert
        //    assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
        //    assertProccessErrors(processResults.Trace);
        //}



        //[Test]
        //public void DeliveryEnv_IsHistoryExecutedFilesChanged_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ChangedHistoryFiles_Incremental")] ProjectConfigItemForTestBase projectConfig)
        //{
        //    //Arrange
        //    _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

        //    RemoveArtifactTempFolder(projectConfig);
        //    string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
        //    restoreDB(projectConfig, dbBackupFileFileFullPath);

        //    NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


        //    //Act
        //    ProcessResults processResults = AutoVersionsDbAPI.ValidateDBVersions(projectConfig.Id, null);


        //    //Assert
        //    assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
        //    Assert.That(processResults.Trace.HasError);
        //    Assert.That(processResults.Trace.ContainErrorCode("HistoryExecutedFilesChanged"));
        //}


        //[Test]
        //public void DeliveryEnv_ScriptsFilesAndDBExecutionHistoryIsMatch_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_MissingFile")] ProjectConfigItemForTestBase projectConfig)
        //{
        //    //Arrange
        //    _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

        //    RemoveArtifactTempFolder(projectConfig);
        //    string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
        //    restoreDB(projectConfig, dbBackupFileFileFullPath);

        //    NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

        //    //Act
        //    ProcessResults processResults = AutoVersionsDbAPI.ValidateDBVersions(projectConfig.Id, null);


        //    //Assert
        //    assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
        //    Assert.That(processResults.Trace.HasError);
        //    Assert.That(processResults.Trace.ContainErrorCode("HistoryExecutedFilesChanged"));
        //}


        //[Test]
        //public void DeliveryEnv_TablesExist_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        //{
        //    //Arrange
        //    _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

        //    RemoveArtifactTempFolder(projectConfig);
        //    string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_MissingSystemTables.bak");
        //    restoreDB(projectConfig, dbBackupFileFileFullPath);

        //    NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


        //    //Act
        //    ProcessResults processResults = AutoVersionsDbAPI.ValidateDBVersions(projectConfig.Id, null);


        //    //Assert
        //    assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
        //    Assert.That(processResults.Trace.HasError);
        //    Assert.That(processResults.Trace.ContainErrorCode("SystemTables"));
        //}


        ////Comment: TargetStateScript is not relevant on delivery evnironment, so we dont need to test it


        //[Test]
        //public void DeliveryEnv_ValidateArtifactFile_NotValid([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        //{
        //    //Arrange
        //    _mockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);

        //    RemoveArtifactTempFolder(projectConfig);
        //    projectConfig.DeliveryArtifactFolderPath += "_NotExistFolderSuffix";

        //    NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

        //    //Act
        //    ProcessResults processResults = AutoVersionsDbAPI.ValidateDBVersions(projectConfig.Id, null);


        //    //Assert
        //    assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
        //    Assert.That(processResults.Trace.HasError);
        //    Assert.That(processResults.Trace.ContainErrorCode("ArtifactFile"));
        //}





    }
}
