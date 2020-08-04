using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using NuGet.Frameworks;
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
            //Arrange
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                IsDevEnvironment = true
            };



            //Act
            ProcessStateResults processResult = AutoVersionsDbAPI.ValidateProjectConfig(projectConfig, null);


            //Assert
            Assert.That(processResult.HasError);

            List<NotificationStateItem> notificationStatesHistory = processResult.StatesHistory;
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
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessStateResults processResult = AutoVersionsDbAPI.ValidateProjectConfig(projectConfig, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertProccessErrors(processResult);
        }


        [Test]
        public void DevEnv_ValidateAll_Valid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            ProcessStateResults processResult = AutoVersionsDbAPI.ValidateAll(projectConfig, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            assertProccessErrors(processResult);
        }



        [Test]
        public void DevEnv_IsHistoryExecutedFilesChanged_NotValid([ValueSource("ProjectConfigItemArray_DevEnv_ChangedHistoryFiles_Incremental")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);
       
            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            ProcessStateResults processResult = AutoVersionsDbAPI.ValidateAll(projectConfig, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processResult.HasError);
            Assert.That(processResult.ErrorCode == "IsHistoryExecutedFilesChanged");
        }


        [Test]
        public void DevEnv_ScriptsFilesAndDBExecutionHistoryIsMatch_NotValid([ValueSource("ProjectConfigItemArray_DevEnv_MissingFile")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);
         
            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            ProcessStateResults processResult = AutoVersionsDbAPI.ValidateAll(projectConfig, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processResult.HasError);
            Assert.That(processResult.ErrorCode == "IsHistoryExecutedFilesChanged");
        }


        [Test]
        public void DevEnv_TablesExist_NotValid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_MissingSystemTables.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            ProcessStateResults processResult = AutoVersionsDbAPI.ValidateAll(projectConfig, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processResult.HasError);
            Assert.That(processResult.ErrorCode == "SystemTables");
        }


        [Test]
        public void DevEnv_TargetStateScriptShouldNotBeHistorical_Valid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_MiddleState__incScript_2020-02-25.102_CreateLookupTable2.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            bool isValid = AutoVersionsDbAPI.ValdiateTargetStateAlreadyExecuted(projectConfig, c_targetStateFile_FinalState,null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(isValid);
        }

        [Test]
        public void DevEnv_TargetStateScriptShouldNotBeHistorical_NotValid([ValueSource("ProjectConfigItemArray_DevEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            string dbBackupFileFileFullPath = Path.Combine(FileSystemHelpers.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DevEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);
       
            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);


            //Act
            bool isValid = AutoVersionsDbAPI.ValdiateTargetStateAlreadyExecuted(projectConfig, c_targetStateFile_MiddleState, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(!isValid);
        }



    }
}
