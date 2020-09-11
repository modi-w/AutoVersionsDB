using AutoVersionsDB.Common;
using AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests.ProjectConfigItemForTests;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using NUnit.Framework;
using System.IO;

namespace AutoVersionsDB.Core.IntegrationTests.AutoVersionsDbAPI_Tests
{
    [TestFixture]
    public class AutoVersionsDbAPI_DeliveryEnv_NotAollowMethods_Tests : AutoVersionsDbAPI_TestsBase
    {


        [Test]
        public void SetDBToSpecificState__Should_NotAllow([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
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
            Assert.That(processTrace.ErrorCode == "DeliveryEnvironment");
        }

        [Test]
        public void Deploy__Should_NotAllow([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);
            _mockProjectConfigs.Setup(m => m.IsProjectCodeExsit(It.IsAny<string>())).Returns(true);

            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.Deploy(projectConfig.ProjectCode, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processTrace.ErrorCode == "DeliveryEnvironment");
        }

        [Test]
        public void RecreateDBFromScratch__Should_NotAllow([ValueSource("ProjectConfigItemArray_DeliveryEnv_ValidScripts")] ProjectConfigItemForTestBase projectConfig)
        {
            //Arrange
            _mockProjectConfigs.Setup(m => m.GetProjectConfigByProjectCode(It.IsAny<string>())).Returns(projectConfig);
            
            string dbBackupFileFileFullPath = Path.Combine(FileSystemPathUtils.GetDllFolderFullPath(), "DbBackupsForTests", "AutoVersionsDB_FinalState_DeliveryEnv.bak");
            restoreDB(projectConfig, dbBackupFileFileFullPath);

            NumOfConnections numOfOpenConnections_Before = getNumOfOpenConnection(projectConfig);

            //Act
            ProcessTrace processTrace = AutoVersionsDbAPI.RecreateDBFromScratch(projectConfig.ProjectCode, null, null);


            //Assert
            assertNumOfOpenDbConnection(projectConfig, numOfOpenConnections_Before);
            Assert.That(processTrace.ErrorCode == "DeliveryEnvironment");
        }



    }
}
