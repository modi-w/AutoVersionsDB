using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_SyncDB;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Validations;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests.TestDefinitions.DeliveryEnv_Virtual;
using AutoVersionsDB.Core.IntegrationTests.Process;
using AutoVersionsDB.Core.IntegrationTests.ScriptFiles;
using AutoVersionsDB.Helpers;
using AutoVersionsDB.NotificationableEngine;
using Moq;
using Ninject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    [TestFixture]
    public class DeliveryEnv_Validations_Tests
    {
        [SetUp]
        public void Init()
        {
            NinjectUtils_IntegrationTests.CreateKernel();
        }



        [Test]
        //Comment: For this test, we dont have value in DBType, so we dont need to use ProjectConfigsFactory (that create project config for each DBType);
        //          And we dont have CLI Command for ProjectConfigValidation,
        //          So we dont need to use TestsRunner.
        public void DeliveryEnv_ProjectConfigValidation_NotValid()
        {
            //Arrange
            ProjectConfigItem projectConfig = new ProjectConfigItem()
            {
                Id = IntegrationTestsConsts.TestProjectId,
                DevEnvironment = false
            };

            MockObjectsProvider.MockProjectConfigsStorage.Setup(m => m.GetProjectConfigById(It.IsAny<string>())).Returns(projectConfig);



            //Act
            ProcessResults processResults = AutoVersionsDbAPI.ValidateProjectConfig(projectConfig.Id, null);


            //Assert
            ProcessAsserts processAsserts = NinjectUtils_IntegrationTests.NinjectKernelContainer.Get<ProcessAsserts>();

            processAsserts.AssertContainError(this.GetType().Name, processResults.Trace, "DBType");
            processAsserts.AssertContainError(this.GetType().Name, processResults.Trace, "DBName");
            processAsserts.AssertContainError(this.GetType().Name, processResults.Trace, "DBBackupFolderPath");
            processAsserts.AssertContainError(this.GetType().Name, processResults.Trace, "DeliveryArtifactFolderPath");
        }


        [Test]
        public void DeliveryEnv_ProjectConfigValidation_Valid()
        {
            TestsRunner.RunTest<DeliveryEnv_ProjectConfigValidation_API>(false, DBBackupFileType.EmptyDB, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DeliveryEnv_Validate_Valid()
        {
            TestsRunner.RunTest<DeliveryEnv_Validate_Valid_API, DeliveryEnv_Validate_Valid_CLI>(false, DBBackupFileType.MiddleState, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DeliveryEnv_Validate_HistoryExecutedFilesChanged()
        {
            TestsRunner.RunTest<DeliveryEnv_Validate_HistoryExecutedFilesChanged_API, DeliveryEnv_Validate_HistoryExecutedFilesChanged_CLI>(false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.IncrementalChanged);
        }

        [Test]
        public void DeliveryEnv_Validate_HistoryExecutedFileMissing()
        {
            TestsRunner.RunTest<DeliveryEnv_Validate_HistoryExecutedFilesChanged_API, DeliveryEnv_Validate_HistoryExecutedFilesChanged_CLI>(false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.MissingFile);
        }

        [Test]
        public void DeliveryEnv_Validate_MissingSystemTables()
        {
            TestsRunner.RunTest<DeliveryEnv_Validate_MissingSystemTables_API, DeliveryEnv_Validate_MissingSystemTables_CLI>(false, DBBackupFileType.FinalState_MissingSystemTables, ScriptFilesStateType.ValidScripts);
        }


        [Test]
        public void DeliveryEnv_Validate_ArtifactFile()
        {
            TestsRunner.RunTest<DeliveryEnv_Validate_ArtifactFile_API, DeliveryEnv_Validate_ArtifactFile_CLI>(false, DBBackupFileType.FinalState_DeliveryEnv, ScriptFilesStateType.ValidScripts);
        }

    }
}
